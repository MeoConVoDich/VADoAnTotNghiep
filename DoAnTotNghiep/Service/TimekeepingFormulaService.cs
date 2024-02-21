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
    public class TimekeepingFormulaService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public TimekeepingFormulaService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<TimekeepingFormula> CreateFilter(TimekeepingFormulaSearch model, ISession session)
        {
            var query = session.Query<TimekeepingFormula>();
            if (model.Id.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Id == model.Id);
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
            else
            {
                if (model.Code.IsNotNullOrEmpty())
                {
                    query = query.Where(c => c.Name.Contains(model.Code));
                }
            }
            return query;
        }

        public async Task<(List<TimekeepingFormula>, int)> GetAllWithFilterAsync(TimekeepingFormulaSearch model)
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

        public async Task<(List<TimekeepingFormula>, int)> GetPageWithFilterAsync(TimekeepingFormulaSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.OrderBy(c => c.Code).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
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

        public async Task<bool> DeleteAsync(TimekeepingFormula model)
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

        public async Task<bool> DeleteListAsync(List<TimekeepingFormula> users)
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

        public async Task<bool> UpdateAsync(TimekeepingFormula model)
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

        public async Task<bool> AddAsync(TimekeepingFormula model)
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

        public async Task<int> GetCountCode()
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var countCode = await session.Query<TimekeepingFormula>().MaxAsync(c => c.CountCode);
                        transaction.Commit();
                        return countCode + 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
