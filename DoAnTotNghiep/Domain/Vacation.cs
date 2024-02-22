using DoAnTotNghiep.Config;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class Vacation
    {
        public virtual string Id { get; set; }
        public virtual Users UsersData { get => Users.IsProxy() ? new Users() : Users; set { } }
        public virtual Users Users { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual decimal? NumberOfDays { get; set; }
        public virtual string Reason { get; set; }
        public virtual string NotApprovedReason { get; set; }
        public virtual string TimekeepingTypeId { get; set; }
        public virtual ChooseBreak ChooseBreak { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public virtual DateTime? ApprovedDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual CreatorObject CreatorObject { get; set; }
    }
}
