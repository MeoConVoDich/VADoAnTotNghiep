using AutoMapper;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace DoAnTotNghiep.Service
{
    public class SummaryOfTimekeepingService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public SummaryOfTimekeepingService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<SummaryOfTimekeeping> CreateFilter(SummaryOfTimekeepingSearch model, ISession session)
        {
            var query = session.Query<SummaryOfTimekeeping>();
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

        public async Task<SummaryOfTimekeeping> GetWithFilterAsync(SummaryOfTimekeepingSearch search)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var filter = CreateFilter(search, session);
                        var data = await filter.FirstOrDefaultAsync();
                        transaction.Commit();
                        return data ?? new SummaryOfTimekeeping();
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        return new SummaryOfTimekeeping();
                        throw;
                    }
                }
            }
        }
    }
}
