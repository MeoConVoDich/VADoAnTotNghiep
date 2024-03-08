using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class SummaryOfTimekeeping
    {
        public virtual string Id { get; set; }
        public virtual string UsersId { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual int? StandradDay { get; set; }
        public virtual string DataType { get; set; }
        public virtual string DataFormula { get; set; }
        public virtual int? WorkLateMinutes { get; set; }
        public virtual int? LeaveEarlyMinutes { get; set; }
        public virtual decimal? OvertimeHour { get; set; }
    }
}
