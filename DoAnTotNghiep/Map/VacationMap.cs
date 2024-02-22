using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class VacationMap : ClassMapping<Vacation>
    {
        public VacationMap()
        {
            Table("Vacation");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.StartDate);
            Property(x => x.EndDate);
            Property(x => x.NumberOfDays);
            Property(x => x.Reason);
            Property(x => x.NotApprovedReason);
            Property(x => x.TimekeepingTypeId);
            Property(x => x.ChooseBreak);
            Property(x => x.ApprovalStatus);
            Property(x => x.ApprovedDate);
            Property(x => x.CreateDate);
            Property(x => x.CreatorObject);
            ManyToOne(x => x.Users, map =>
            {
                map.Column("UsersId");
                map.Class(typeof(Users));
            });
        }
    }
}