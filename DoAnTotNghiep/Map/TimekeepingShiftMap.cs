using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class TimekeepingShiftMap : ClassMapping<TimekeepingShift>
    {
        public TimekeepingShiftMap()
        {
            Table("TimekeepingShift");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Code);
            Property(x => x.Name);
            Property(x => x.StartTime);
            Property(x => x.EndTime);
            Property(x => x.StartBreaksTime);
            Property(x => x.EndBreaksTime);
            Property(x => x.Duration);
            Property(x => x.TimekeepingTypeFull);
            Property(x => x.TimekeepingTypeFirst);
            Property(x => x.TimekeepingTypeSecond);
            Property(x => x.TimekeepingTypeOff);
            Property(x => x.CreateDate);
            Property(x => x.EffectiveState);
            Property(x => x.BreaksTimeType);
        }
    }
}
