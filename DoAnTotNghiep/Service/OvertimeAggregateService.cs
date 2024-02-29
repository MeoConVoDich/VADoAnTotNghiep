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
    public class OvertimeAggregateService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public OvertimeAggregateService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<OvertimeAggregate> CreateFilter(OvertimeAggregateSearch model, ISession session)
        {
            var query = session.Query<OvertimeAggregate>();
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

        public async Task<(List<OvertimeAggregate>, int)> GetAllWithFilterAsync(OvertimeAggregateSearch search)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var filter = CreateFilter(search, session);
                        var data = await filter.OrderBy(c => c.RegisterDate).OrderBy(c => c.StartTime).ToListAsync();
                        var count = await filter.CountAsync();
                        transaction.Commit();
                        return (data, count);
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        return (new List<OvertimeAggregate>(), 0);
                        throw;
                    }
                }
            }
        }

        public async Task<bool> AggregateAsync(OvertimeAggregateSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<string> usersIds = new List<string>();
                        if (model.UsersIds?.Any() == true)
                        {
                            usersIds = model.UsersIds;
                        }
                        else
                        {
                            var filterUsers = session.Query<Users>();
                            if (model.CodeOrName.IsNotNullOrEmpty())
                            {
                                filterUsers = filterUsers.Where(c => c.Code.Contains(model.CodeOrName) || c.Name.Contains(model.CodeOrName));
                            }
                            var usersData = await filterUsers.ToListAsync();
                            usersIds = usersData.Select(c => c.Id).ToList();
                        }
                        List<OvertimeAggregate> overtimeAggregates = new List<OvertimeAggregate>();
                        var overtimeRate = await session.Query<OvertimeRate>()
                            .Where(c => c.EffectiveState == EffectiveState.Active)
                            .Where(c => c.Date <= DateTime.Now)
                            .OrderByDescending(c => c.Date).ThenBy(c => c.CreateDate).FirstOrDefaultAsync();
                        int j = 0;
                        int batchSize = 200;
                        while (usersIds.Count >= j * batchSize)
                        {
                            var usersBatchIds = usersIds.Skip(j * batchSize).Take(batchSize).ToList();
                            j++;
                            await session.Query<OvertimeAggregate>()
                                .Where(c => c.Year == model.Year)
                                .Where(c => c.Month == model.Month)
                                .Where(c => usersBatchIds.Contains(c.UsersId))
                                .DeleteAsync();
                            var Overtimes = await session.Query<Overtime>()
                                .Where(c => usersBatchIds.Contains(c.Users.Id))
                                .Where(c => c.ApprovalStatus == ApprovalStatus.Approved)
                                .Where(c => c.RegisterDate.Value.Month == model.Month)
                                .Where(c => c.RegisterDate.Value.Year == model.Year)
                                .ToListAsync();

                            foreach (var usersId in usersBatchIds)
                            {
                                var OvertimeUsers = Overtimes.Where(c => c.Users.Id == usersId);
                                foreach (var ot in OvertimeUsers)
                                {
                                    OvertimeAggregate dt = _mapper.Map<OvertimeAggregate>(ot);
                                    dt.Month = model.Month.Value;
                                    dt.Year = model.Year.Value;
                                    if (overtimeRate != null)
                                    {
                                        if (dt.OvertimeType == OvertimeType.NormalDay)
                                        {
                                            dt.DayHourCoefficientAmount = dt.DayHourAmount * (overtimeRate.Day ?? 100.0M) / 100.0M;
                                            dt.NightHourCoefficientAmount = dt.NightHourAmount * (overtimeRate.Night ?? 100.0M) / 100.0M;
                                        }
                                        else if(dt.OvertimeType == OvertimeType.WeeklyRestDay)
                                        {
                                            dt.DayHourCoefficientAmount = dt.DayHourAmount * (overtimeRate.DayOff ?? 100.0M) / 100.0M;
                                            dt.NightHourCoefficientAmount = dt.NightHourAmount * (overtimeRate.NightOff ?? 100.0M) / 100.0M;
                                        }
                                        else if (dt.OvertimeType == OvertimeType.Holiday)
                                        {
                                            dt.DayHourCoefficientAmount = dt.DayHourAmount * (overtimeRate.DayHoliday ?? 100.0M) / 100.0M;
                                            dt.NightHourCoefficientAmount = dt.NightHourAmount * (overtimeRate.NightHoliday ?? 100.0M) / 100.0M;
                                        }
                                    }
                                    else
                                    {
                                        dt.DayHourCoefficientAmount = dt.DayHourAmount;
                                        dt.NightHourCoefficientAmount = dt.NightHourAmount;
                                    }
                                    dt.Total = dt.DayHourCoefficientAmount + dt.NightHourCoefficientAmount;
                                    if (dt.Total > 0)
                                    {
                                        overtimeAggregates.Add(dt);
                                    }
                                }
                            }
                        }
                        foreach (var item in overtimeAggregates)
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
