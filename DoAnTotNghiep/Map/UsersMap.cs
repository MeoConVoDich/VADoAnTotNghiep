using DoAnTotNghiep.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class UsersMap : ClassMap<Users>
    {
		public UsersMap()
		{
			Table("Users");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Assigned().Column("Id");
			Map(x => x.UserName).Column("UserName").Not.Nullable();
			Map(x => x.Password).Column("Password").Not.Nullable();
			Map(x => x.IsAdmin).Column("IsAdmin");
			Map(x => x.Permission).Column("Permission");
			Map(x => x.PermissionGroup).Column("PermissionGroup");
		}
	}
}
