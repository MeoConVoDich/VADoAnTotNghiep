using DoAnTotNghiep.Config;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class Overtime
    {
        public virtual string Id { get; set; }
        public virtual Users UsersData { get => Users.IsProxy() ? new Users() : Users; set { } }
        public virtual Users Users { get; set; }
        public virtual DateTime? RegisterDate { get; set; }
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual DateTime? StartBreakTime { get; set; }
        public virtual DateTime? EndBreakTime { get; set; }
        public virtual decimal? TotalHour { get; set; }
        public virtual decimal? DayHourAmount { get; set; }
        public virtual decimal? NightHourAmount { get; set; }
        public virtual string Reason { get; set; }
        public virtual string DisapprovedReason { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public virtual DateTime? ApprovedDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual CreatorObject CreatorObject { get; set; }
        public virtual BreaksTimeType BreaksTimeType { get; set; }
    }
}
