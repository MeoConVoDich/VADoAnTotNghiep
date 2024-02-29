using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class OvertimeAggregateMap : ClassMapping<OvertimeAggregate>
    {
        public OvertimeAggregateMap()
        {
            Table("OvertimeAggregate");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Year);
            Property(x => x.Month);
            Property(x => x.UsersId);
            Property(x => x.RegisterDate);
            Property(x => x.DayHourAmount);
            Property(x => x.NightHourAmount);
            Property(x => x.DayHourCoefficientAmount);
            Property(x => x.NightHourCoefficientAmount);
            Property(x => x.Total);
            Property(x => x.StartTime);
            Property(x => x.EndTime);
            Property(x => x.StartBreakTime);
            Property(x => x.EndBreakTime);
            Property(x => x.Reason);
            Property(x => x.OvertimeType);
        }
    }
}