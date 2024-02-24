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
    public class WorkShiftTableService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public WorkShiftTableService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<Users> CreateFilterUsers(WorkShiftTableSearch model, ISession session)
        {
            var query = session.Query<Users>();
            if (model.CodeOrName.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Name.Contains(model.CodeOrName) || c.Code.Contains(model.CodeOrName));
            }
            if (model.UsersId.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.Id == model.UsersId);
            }
            return query;
        }

        IQueryable<WorkShiftTable> CreateFilterWorkShiftTable(WorkShiftTableSearch model, ISession session)
        {
            var query = session.Query<WorkShiftTable>();
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

        public async Task<(List<WorkShiftTable>, int)> GetAllWithFilterAsync(WorkShiftTableSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var queryUsers = CreateFilterUsers(model, session);
                        var dataUsers = await queryUsers.OrderBy(c => c.Name).ToListAsync();
                        var usersIds = dataUsers.Select(c => c.Id).ToList();

                        var queryWorkShiftTable = CreateFilterWorkShiftTable(model, session);
                        var dataShiftTable = await queryWorkShiftTable.ToListByListKeyAsync(usersIds, nameof(WorkShiftTable.UsersId));

                        var data = dataUsers
                            .GroupJoin(dataShiftTable, u => u.Id, s => s.UsersId, (u, s) => new { u, s })
                            .SelectMany(p => p.s.DefaultIfEmpty(),
                                (u, s) =>
                                {
                                   WorkShiftTable workShiftTable = s ?? new WorkShiftTable
                                   {
                                       UsersId = u.u.Id,
                                       Year = model.Year.Value,
                                       Month = model.Month.Value,
                                   };
                                   workShiftTable.Users = u.u;
                                   return workShiftTable;
                               }
                            ).ToList();
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

        public async Task<(List<WorkShiftTable>, int)> GetPageWithFilterAsync(WorkShiftTableSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var queryUsers = CreateFilterUsers(model, session);
                        var dataUsers = await queryUsers.OrderBy(c => c.Name).Skip((model.Page.PageIndex - 1) * model.Page.PageSize).Take(model.Page.PageSize).ToListAsync();
                        var count = await queryUsers.CountAsync();
                        var usersIds = dataUsers.Select(c => c.Id).ToList();

                        var queryWorkShiftTable = CreateFilterWorkShiftTable(model, session);
                        var dataShiftTable = await queryWorkShiftTable.ToListByListKeyAsync(usersIds, nameof(WorkShiftTable.UsersId));

                        var data = dataUsers
                            .GroupJoin(dataShiftTable, u => u.Id, s => s.UsersId, (u, s) => new { u, s })
                            .SelectMany(p => p.s.DefaultIfEmpty(),
                                (u, s) =>
                                {
                                    WorkShiftTable workShiftTable = s ?? new WorkShiftTable
                                    {
                                        UsersId = u.u.Id,
                                        Year = model.Year.Value,
                                        Month = model.Month.Value,
                                    };
                                    workShiftTable.Users = u.u;
                                    return workShiftTable;
                                }
                            ).ToList();
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

        public async Task<bool> DeleteAsync(WorkShiftTable model)
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

        public async Task<bool> DeleteListAsync(List<WorkShiftTable> datas)
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

        public async Task<bool> UpdateAsync(WorkShiftTable model)
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

        public async Task<bool> UpdateListAsync(List<WorkShiftTable> datas)
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

        public async Task<bool> AddAsync(WorkShiftTable model)
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

        public async Task<bool> AddUpdateListAsync(List<WorkShiftTable> addData, List<WorkShiftTable> updateData)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in updateData)
                        {
                            await session.UpdateAsync(item);
                        }
                        foreach (var item in addData)
                        {
                            await session.SaveAsync(item);
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
    }
}
