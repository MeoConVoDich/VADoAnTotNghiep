using AutoMapper;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.IService;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<(List<Users>, int)> GetAllWithFilterAsync()
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var data = await session.Query<Users>().ToListAsync();
                        transaction.Commit();
                        return (data, 0);
                    }
                    catch (Exception ex)
                    {
                        throw;
                        transaction.Rollback();
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
    }
}
