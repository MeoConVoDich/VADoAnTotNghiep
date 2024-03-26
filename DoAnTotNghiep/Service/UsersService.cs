using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Service
{
    public class UsersService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public UsersService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<Users> CreateFilter(UsersSearch model, ISession session)
        {
            var query = session.Query<Users>();
            if (model.Id.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Id == model.Id);
            }
            if (model.Name.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Name.Contains(model.Name));
            }
            if (model.CodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Name.Contains(model.CodeOrName) || c.Code.Contains(model.CodeOrName));
            }
            if (model.Codes?.Any() == true)
            {
                query = query.Where(c => model.Codes.Contains(c.Code));
            }

            if (model.CheckExist.HasValue && model.CheckExist == true)
            {
                List<Expression<Func<Users, bool>>> compareExpressions = new List<Expression<Func<Users, bool>>>();
                if (model.IdentityNumber.IsNotNullOrEmpty())
                {
                    compareExpressions.Add(c => c.IdentityNumber == model.IdentityNumber);
                }
                if (model.Code.IsNotNullOrEmpty())
                {
                    compareExpressions.Add(c => c.Code == model.Code);
                }
                if (model.UserName.IsNotNullOrEmpty())
                {
                    compareExpressions.Add(c => c.UserName == model.UserName);
                }
                var condition = PredicateBuilder.Create<Users>(x => false);
                if (compareExpressions.Count > 0)
                {
                    foreach (var item in compareExpressions)
                    {
                        condition = condition.Or(item);
                    }
                    query = query.Where(condition);
                }
                query = query.Take(2);
            }
            else
            {
                if (model.Code.IsNotNullOrEmpty())
                {
                    query = query.Where(c => c.Name.Contains(model.Code));
                }
                if (model.UserName.IsNotNullOrEmpty())
                {
                    query = query.Where(c => c.UserName.Contains(model.UserName));
                }
            }
            if (model.GetOnlyNoUserName == true)
            {
                query = query.Where(c => c.UserName != null && c.UserName != "");
            }
            return query;
        }

        public async Task<(List<Users>, int)> GetAllWithFilterAsync(UsersSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.OrderBy(c => c.Name).ToListAsync();
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

        public async Task<(List<Users>, int)> GetPageWithFilterAsync(UsersSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = CreateFilter(model, session);
                        var data = await query.OrderBy(c => c.Name).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
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

        public async Task<bool> DeleteAsync(Users model)
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

        public async Task<bool> DeleteListAsync(List<Users> users)
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

        public async Task<bool> UpdateAsync(Users model)
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

        public async Task<bool> UpdateListAsync(List<Users> updates)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in updates)
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

        public async Task<bool> AddAsync(Users model)
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

        public async Task<Users> GetUsersAsync(string userName, string password)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var data = await session.Query<Users>()
                            .Where(c => c.UserName == userName)
                            .Where(c => c.Password == password)
                            .FirstOrDefaultAsync();
                        transaction.Commit();
                        return data;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }

        public async Task<Users> GetUsersByUserNameAsync(string userName)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var data = await session.Query<Users>()
                            .Where(c => c.UserName == userName)
                            .FirstOrDefaultAsync();
                        transaction.Commit();
                        return data;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
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
                        var model = await session.Query<Users>()
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
