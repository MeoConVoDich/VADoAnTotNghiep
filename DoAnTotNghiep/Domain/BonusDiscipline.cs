using AntDesign;
using DoAnTotNghiep.Config;
using NHibernate.Proxy;
using System;
using System.Xml.Linq;

namespace DoAnTotNghiep.Domain
{
    public class BonusDiscipline
    {
        public virtual string Id { get; set; }
        public virtual Users UsersData { get => Users.IsProxy() ? new Users() : Users; set { } }
        public virtual Users Users { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string PathAttachFile { get; set; }
        public virtual string AttachFileName { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
        public virtual BonusDisciplineType BonusDisciplineType { get; set; }
        public virtual int? Amount { get; set; }
        public virtual string Reason { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
