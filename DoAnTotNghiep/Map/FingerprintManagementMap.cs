using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class FingerprintManagementMap : ClassMapping<FingerprintManagement>
    {
        public FingerprintManagementMap()
        {
            Table("FingerprintManagement");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.UsersId);
            Property(x => x.ScanDate);
            Property(x => x.CreateDate);
            Property(x => x.Year);
            Property(x => x.Month);
            Property(x => x.CreateDate);
        }
    }
}
