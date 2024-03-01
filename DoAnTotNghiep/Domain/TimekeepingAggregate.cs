using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class TimekeepingAggregate
    {
        public virtual string Id { get; set; }
        public virtual string UsersId { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual DateTime? DateOfPhase { get; set; }
        public virtual string ShiftOfPhase { get; set; }
        public virtual DateTime? CheckinDate { get; set; }
        public virtual DateTime? CheckoutDate { get; set; }
        public virtual string TimekeepingCode { get; set; }
        public virtual int? WorkLateMinutes { get; set; }
        public virtual int? LeaveEarlyMinutes { get; set; }
    }
}
