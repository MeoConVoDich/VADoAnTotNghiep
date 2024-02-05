using DoAnTotNghiep.Domain;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class UsersMap : ClassMapping<Users>
    {
		public UsersMap()
        {
            Table("Users");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.UserName);
            Property(x => x.Password);
            Property(x => x.IsAdmin);
            Property(x => x.Permission);
            Property(x => x.PermissionGroup);
        }
	}
}
