using AutoMapper;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace DoAnTotNghiep.Service
{
    public class PermissionGroupService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public PermissionGroupService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<PermissionGroup> CreateFilter(PermissionGroupSearch model, ISession session)
        {
            var query = session.Query<PermissionGroup>();
            if (model.CodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.CodeGroup.Contains(model.CodeOrName) || c.NameGroup.Contains(model.CodeOrName));
            }
            if (model.CheckExist.HasValue && model.CheckExist == true)
            {
                if (model.Code.IsNotNullOrEmpty())
                {
                    query = query.Where(c => c.CodeGroup == model.Code);
                }
                query = query.Take(2);
            }
            return query;
        }

        public async Task<(List<PermissionGroup>, int)> GetAllWithFilterAsync(PermissionGroupSearch model)
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

        public async Task<(List<PermissionGroup>, int)> GetPageWithFilterAsync(PermissionGroupSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
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

        public async Task<bool> DeleteAsync(PermissionGroup model)
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

        public async Task<bool> DeleteListAsync(List<PermissionGroup> users)
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

        public async Task<bool> UpdateAsync(PermissionGroup model)
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

        public async Task<bool> AddAsync(PermissionGroup model)
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

        public async Task<bool> UpdatePermissionAsync(string id, List<string> permissions)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var model = await session.Query<PermissionGroup>()
                            .FirstOrDefaultAsync(c => c.Id == id);
                        model.Permission = JsonSerializer.Serialize(permissions);
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

    }
}
