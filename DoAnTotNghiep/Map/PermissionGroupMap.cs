using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace DoAnTotNghiep.Map
{
    public class PermissionGroupMap : ClassMapping<PermissionGroup>
    {
        public PermissionGroupMap()
        {
            Table("PermissionGroup");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.CodeGroup);
            Property(x => x.NameGroup);
            Property(x => x.Permission);
            Property(x => x.Note);
            Property(x => x.CreateDate);
        }
    }
}
