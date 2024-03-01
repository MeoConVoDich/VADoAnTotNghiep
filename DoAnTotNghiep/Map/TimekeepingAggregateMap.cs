using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class TimekeepingAggregateMap : ClassMapping<TimekeepingAggregate>
    {
        public TimekeepingAggregateMap()
        {
            Table("TimekeepingAggregate");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Year);
            Property(x => x.Month);
            Property(x => x.UsersId);
            Property(x => x.DateOfPhase);
            Property(x => x.ShiftOfPhase);
            Property(x => x.CheckinDate);
            Property(x => x.CheckoutDate);
            Property(x => x.TimekeepingCode);
            Property(x => x.WorkLateMinutes);
            Property(x => x.LeaveEarlyMinutes);
        }
    }
}