using AutoMapper;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.IService;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Service
{
    public class UsersService : Service<Users>, IUsersService
    {
        readonly IMapper _mapper;
        public UsersService(ISession session, IMapper mapper) : base(session) { _mapper = mapper; }

    }
}
