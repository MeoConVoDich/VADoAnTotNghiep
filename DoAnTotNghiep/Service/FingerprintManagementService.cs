using AutoMapper;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Service
{
    public class FingerprintManagementService
    {

        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public FingerprintManagementService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<FingerprintManagement> CreateFilter(FingerprintManagementSearch model, ISession session)
        {
            var query = session.Query<FingerprintManagement>();
            if (model.UsersId.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.UsersId == model.UsersId);
            }
            if (model.UsersIds?.Any() == true)
            {
                query = query.Where(c => model.UsersId.Contains(c.UsersId));
            }
            if (model.Year.HasValue)
            {
                query = query.Where(c => c.Year == model.Year);
            }
            if (model.Month.HasValue)
            {
                query = query.Where(c => c.Month == model.Month);
            }
            return query;
        }

        public async Task<(List<FingerprintGroup>, int)> GetPageWithFilterAsync(FingerprintManagementSearch search)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    int total = 0;
                    var groupData = new List<FingerprintGroup>();
                    try
                    {
                        var filter = CreateFilter(search, session);
                        var lookup = filter.OrderBy(c => c.ScanDate)
                                    .ToLookup(c => c.UsersId).AsEnumerable();
                        var usersIds = lookup.Select(c => c.Key).ToList();
                        var listUsers = await session.Query<Users>().ToListByListKeyAsync(usersIds, nameof(Users.Id));
                        total = lookup.Count();
                        foreach (var item in lookup)
                        {
                            var grs = lookup.Single(c => c.Key == item.Key);
                            var lockupByDate = grs.ToLookup(c => c.ScanDate.Value.Date).AsEnumerable();
                            foreach (var gr in lockupByDate)
                            {
                                groupData.Add(new FingerprintGroup()
                                {
                                    Users = listUsers.FirstOrDefault(c => c.Id == item.Key),
                                    FingerprintManagements = gr.ToList()
                                });
                            }
                        }
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                    }
                    return (groupData, total);
                }
            }
        }

        public async Task<(List<FingerprintManagement>, List<Users>)> GetAllWithFilterAsync(FingerprintManagementSearch search)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    List<FingerprintManagement> fingerprints = new List<FingerprintManagement>();
                    List<Users> usersData = new List<Users>();
                    try
                    {
                        usersData = await session.Query<Users>().ToListAsync();
                        int i = 0;
                        int batchSize = 200;
                        while (usersData.Count >= i * batchSize)
                        {
                            var usersBatchs = usersData.Skip(i * batchSize).Take(batchSize).ToList();
                            var usersBatchIds = usersBatchs.Select(c => c.Id).ToList();
                            var fingerprintManagements = await session.Query<FingerprintManagement>().Where(c => c.Month == search.Month
                                && c.Year == search.Year && usersBatchIds.Contains(c.UsersId)).ToListAsync();
                            fingerprintManagements = fingerprintManagements.GroupJoin(usersBatchs, f => f.UsersId, s => s.Id, (f, s) => (f, s))
                            .Select(x =>
                            {
                                x.f.Users = x.s.FirstOrDefault();
                                return x.f;
                            }).ToList();
                            fingerprints.AddRange(fingerprintManagements);
                            i++;
                        }
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                    }
                    return (fingerprints, usersData);
                }
            }
        }

        public async Task<bool> AddListAsync(List<FingerprintManagement> data)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = 0;
                        int year = data.FirstOrDefault().Year;
                        int month = data.FirstOrDefault().Month;
                        var usersIds = data.Select(c => c.UsersId).Distinct().ToList();
                        var limitList = 1000;
                        while (usersIds.Count >= i * limitList)
                        {
                            var ids = usersIds.Skip(i * limitList).Take(limitList).ToList();
                            await session.Query<FingerprintManagement>()
                                .Where(c => c.Year == year && c.Month == month
                                    && ids.Contains(c.UsersId))
                                .DeleteAsync(CancellationToken.None);
                            i++;
                        }
                        foreach (var item in data)
                        {
                            await session.SaveAsync(item);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
