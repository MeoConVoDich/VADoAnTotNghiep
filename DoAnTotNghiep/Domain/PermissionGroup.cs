using DoAnTotNghiep.Config;
using NHibernate.Proxy;
using System;

namespace DoAnTotNghiep.Domain
{
    public class PermissionGroup
    {
        public virtual string Id { get; set; }
        public virtual string CodeGroup { get; set; }
        public virtual string NameGroup { get; set; }
        public virtual string Permission { get; set; }
        public virtual string Note { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
