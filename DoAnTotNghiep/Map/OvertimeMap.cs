using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class OvertimeMap : ClassMapping<Overtime>
    {
        public OvertimeMap()
        {
            Table("Overtime");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.RegisterDate);
            Property(x => x.StartTime);
            Property(x => x.EndTime);
            Property(x => x.StartBreakTime);
            Property(x => x.EndBreakTime);
            Property(x => x.TotalHour);
            Property(x => x.DayHourAmount);
            Property(x => x.NightHourAmount);
            Property(x => x.Reason);
            Property(x => x.DisapprovedReason);
            Property(x => x.ApprovalStatus);
            Property(x => x.ApprovedDate);
            Property(x => x.CreateDate);
            Property(x => x.CreatorObject);
            Property(x => x.BreaksTimeType);
            Property(x => x.OvertimeType);
            ManyToOne(x => x.Users, map =>
            {
                map.Column("UsersId");
                map.Class(typeof(Users));
            });
        }
    }
}