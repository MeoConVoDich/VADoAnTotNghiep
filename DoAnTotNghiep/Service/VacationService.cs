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
    public class VacationService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public VacationService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<Vacation> CreateFilter(VacationSearch model, ISession session)
        {
            var query = session.Query<Vacation>();
            if (model.Year.HasValue)
            {
                query = query.Where(c => c.StartDate.Year == model.Year || c.EndDate.Year == model.Year);
            }
            if (model.Month.HasValue)
            {
                query = query.Where(c => c.StartDate.Month == model.Month || c.EndDate.Month == model.Month);
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
                if (model.StartDateCheckExist.HasValue && model.EndDateCheckExist.HasValue)
                {
                    query = query.Where(c => (c.StartDate <= model.EndDateCheckExist.Value.Date && model.EndDateCheckExist.Value.Date <= c.EndDate)
                        || (c.StartDate <= model.StartDateCheckExist.Value.Date && model.StartDateCheckExist.Value.Date <= c.EndDate)
                        || (model.StartDateCheckExist.Value.Date < c.EndDate && c.EndDate < model.EndDateCheckExist.Value.Date));
                    query = query.Where(c => c.ApprovalStatus == ApprovalStatus.Approved|| c.ApprovalStatus == ApprovalStatus.Pending);
                }
                query = query.Take(2);
            }
            return query;
        }

        public async Task<(List<Vacation>, int)> GetAllWithFilterAsync(VacationSearch model)
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

        public async Task<(List<Vacation>, int)> GetPageWithFilterAsync(VacationSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.OrderByDescending(c => c.StartDate).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
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

        public async Task<bool> DeleteAsync(Vacation model)
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

        public async Task<bool> DeleteListAsync(List<Vacation> users)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in users)
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

        public async Task<bool> UpdateAsync(Vacation model)
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

        public async Task<bool> AddAsync(Vacation model)
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
