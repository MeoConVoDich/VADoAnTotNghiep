using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class SummaryOfTimekeepingMap : ClassMapping<SummaryOfTimekeeping>
    {
        public SummaryOfTimekeepingMap()
        {
            Table("SummaryOfTimekeeping");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Year);
            Property(x => x.Month);
            Property(x => x.UsersId);
            Property(x => x.StandradDay);
            Property(x => x.DataType);
            Property(x => x.DataFormula);
            Property(x => x.WorkLateMinutes);
            Property(x => x.LeaveEarlyMinutes);
            Property(x => x.OvertimeHour);
        }
    }
}
