using AutoMapper;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System.Linq.Expressions;
using System.Linq;
using NHibernate.Linq;

namespace DoAnTotNghiep.Service
{
    public class BonusDisciplineService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public BonusDisciplineService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<BonusDiscipline> CreateFilter(BonusDisciplineSearch model, ISession session)
        {
            var query = session.Query<BonusDiscipline>();
            if (model.UsersId.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Users.Id == model.UsersId);
            }
            if (model.StaffCodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Users.Name.Contains(model.StaffCodeOrName) || c.Users.Code.Contains(model.StaffCodeOrName));
            }
            if (model.CodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Name.Contains(model.CodeOrName) || c.Code.Contains(model.CodeOrName));
            }
            if (model.CheckExist.HasValue && model.CheckExist == true)
            {
                if (model.Code.IsNotNullOrEmpty())
                {
                    query = query.Where(c => c.Code == model.Code);
                }
                query = query.Take(2);
            }
            if (model.FromDate.HasValue)
            {
                query = query.Where(c => c.Date > model.FromDate.Value.Date.AddMilliseconds(-1));
            }
            if (model.ToDate.HasValue)
            {
                query = query.Where(c => c.Date < model.ToDate.Value.Date.AddDays(1));
            }
            if (model.BonusDisciplineType != BonusDisciplineType.All)
            {
                query = query.Where(c => c.BonusDisciplineType == model.BonusDisciplineType);
            }
            if (model.EffectiveState != EffectiveState.All)
            {
                query = query.Where(c => c.EffectiveState == model.EffectiveState);
            }
            return query;
        }

        public async Task<(List<BonusDiscipline>, int)> GetAllWithFilterAsync(BonusDisciplineSearch model)
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

        public async Task<(List<BonusDiscipline>, int)> GetPageWithFilterAsync(BonusDisciplineSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.Fetch(c => c.Users).OrderByDescending(c => c.EffectiveState).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
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

        public async Task<bool> DeleteAsync(BonusDiscipline model)
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

        public async Task<bool> DeleteListAsync(List<BonusDiscipline> users)
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

        public async Task<bool> UpdateAsync(BonusDiscipline model)
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

        public async Task<bool> AddAsync(BonusDiscipline model)
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
