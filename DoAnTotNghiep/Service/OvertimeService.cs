using AutoMapper;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Service
{
    public class OvertimeService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public OvertimeService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<Overtime> CreateFilter(OvertimeSearch model, ISession session)
        {
            var query = session.Query<Overtime>();
            if (model.Year.HasValue)
            {
                query = query.Where(c => c.RegisterDate.Value.Year == model.Year);
            }
            if (model.Month.HasValue)
            {
                query = query.Where(c => c.RegisterDate.Value.Month == model.Month);
            }
            if (model.ApprovalStatus != ApprovalStatus.All)
            {
                query = query.Where(c => c.ApprovalStatus == model.ApprovalStatus);
            }
            if (model.CodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Users.Name.Contains(model.CodeOrName) || c.Users.Code.Contains(model.CodeOrName));
            }
            if (model.UsersId.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Users.Id == model.UsersId);
            }
            if (model.CheckExist.HasValue && model.CheckExist == true)
            {
                if (model.RegisterDate.HasValue)
                {
                    query = query.Where(c => c.RegisterDate.Value.Date == model.RegisterDate.Value.Date);
                }
                if (model.StartTime.HasValue && model.EndTime.HasValue)
                {
                    query = query.Where(c => (c.StartTime.Value <= model.EndTime.Value && model.EndTime.Value <= c.EndTime.Value)
                        || (c.StartTime.Value <= model.StartTime.Value && model.StartTime.Value <= c.EndTime.Value)
                        || (model.StartTime.Value < c.EndTime.Value && c.EndTime.Value < model.EndTime.Value));
                    query = query.Where(c => c.ApprovalStatus == ApprovalStatus.Approved || c.ApprovalStatus == ApprovalStatus.Pending);
                }
                query = query.Take(2);
            }
            return query;
        }

        public async Task<(List<Overtime>, int)> GetAllWithFilterAsync(OvertimeSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.ToListAsync();
                        transaction.Commit();
                        return (data, 0);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }

        public async Task<(List<Overtime>, int)> GetPageWithFilterAsync(OvertimeSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.Fetch(c => c.Users).OrderByDescending(c => c.RegisterDate).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
                        var count = await query.CountAsync();
                        transaction.Commit();
                        return (data, count);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }

        public async Task<bool> DeleteAsync(Overtime model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        await session.DeleteAsync(model);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> DeleteListAsync(List<Overtime> datas)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in datas)
                        {
                            await session.DeleteAsync(item);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> UpdateAsync(Overtime model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        await session.UpdateAsync(model);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> UpdateListAsync(List<Overtime> datas)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in datas)
                        {
                            await session.UpdateAsync(item);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> AddAsync(Overtime model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        await session.SaveAsync(model);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
