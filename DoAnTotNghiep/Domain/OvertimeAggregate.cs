using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class OvertimeAggregate
    {
        public virtual string Id { get; set; }
        public virtual string UsersId { get; set; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual decimal DayHourAmount { get; set; }
        public virtual decimal NightHourAmount { get; set; }
        public virtual decimal DayHourCoefficientAmount { get; set; }
        public virtual decimal NightHourCoefficientAmount { get; set; }
        public virtual decimal Total { get; set; }
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual DateTime? StartBreakTime { get; set; }
        public virtual DateTime? EndBreakTime { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual Users Users { get; set; }
        public virtual string Reason { get; set; }
        public virtual OvertimeType OvertimeType { get; set; }
    }
}
