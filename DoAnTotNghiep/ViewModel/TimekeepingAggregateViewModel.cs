using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class TimekeepingAggregateViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }
        public virtual string UsersId { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }

        [Display(Name = "Ngày")]
        public virtual DateTime? DateOfPhase { get; set; }

        [Display(Name = "Ca làm việc")]
        public virtual string ShiftOfPhase { get; set; }

        [Display(Name = "Giờ vào")]
        public virtual DateTime? CheckinDate { get; set; }

        [Display(Name = "Giờ ra")]
        public virtual DateTime? CheckoutDate { get; set; }

        [Display(Name = "Mã công")]
        public virtual string TimekeepingCode { get; set; }

        [Display(Name = "Số phút đi muộn")]
        public virtual int? WorkLateMinutes { get; set; }

        [Display(Name = "Số phút về sớm")]
        public virtual int? LeaveEarlyMinutes { get; set; }
    }
}
