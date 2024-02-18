using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace DoAnTotNghiep.Map
{
    public class BonusDisciplineMap : ClassMapping<BonusDiscipline>
    {
        public BonusDisciplineMap()
        {
            Table("BonusDiscipline");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.UsersId);
            Property(x => x.Name);
            Property(x => x.Code);
            Property(x => x.PathAttachFile);
            Property(x => x.AttachFileName);
            Property(x => x.EffectiveState);
            Property(x => x.BonusDisciplineType);
            Property(x => x.Amount);
            Property(x => x.Reason);
            Property(x => x.CreateDate);
            Property(x => x.Date);
        }
    }
}
