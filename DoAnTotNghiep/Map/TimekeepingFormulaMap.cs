using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class TimekeepingFormulaMap : ClassMapping<TimekeepingFormula>
    {
        public TimekeepingFormulaMap()
        {
            Table("TimekeepingFormula");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Code);
            Property(x => x.Name);
            Property(x => x.CountCode);
            Property(x => x.Formula);
            Property(x => x.CreateDate);
        }
    }
}
