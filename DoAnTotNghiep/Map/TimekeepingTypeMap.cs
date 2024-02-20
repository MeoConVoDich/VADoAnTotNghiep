using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class TimekeepingTypeMap : ClassMapping<TimekeepingType>
    {
        public TimekeepingTypeMap()
        {
            Table("TimekeepingType");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Code);
            Property(x => x.Name);
            Property(x => x.Note);
            Property(x => x.EffectiveState);
            Property(x => x.CreateDate);
        }
    }
}
