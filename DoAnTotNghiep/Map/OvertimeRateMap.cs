using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class OvertimeRateMap : ClassMapping<OvertimeRate>
    {
        public OvertimeRateMap()
        {
            Table("OvertimeRate");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Day);
            Property(x => x.Night);
            Property(x => x.CreateDate);
            Property(x => x.Date);
            Property(x => x.EffectiveState);
        }
    }
}
