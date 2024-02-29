using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class OvertimeAggregateViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }
        public virtual string UsersId { get; set; }

        [Display(Name = "Ngày làm việc")]
        public virtual DateTime RegisterDate { get; set; }
        [Display(Name = "Số giờ làm ngày thực tế")]
        public virtual decimal DayHourAmount { get; set; }
        [Display(Name = "Số giờ làm đêm thực tế")]
        public virtual decimal NightHourAmount { get; set; }
        [Display(Name = "Số giờ làm ngày nhân hệ số")]
        public virtual decimal DayHourCoefficientAmount { get; set; }
        [Display(Name = "Số giờ làm đêm nhân hệ số")]
        public virtual decimal NightHourCoefficientAmount { get; set; }
        [Display(Name = "Tổng giờ nhân hệ số")]
        public virtual decimal Total { get; set; }
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Thời gian làm")]
        public virtual string WorkTime { get; set; }
        public virtual DateTime? StartBreakTime { get; set; }
        public virtual DateTime? EndBreakTime { get; set; }

        [Display(Name = "Nghỉ giữa ca")]
        public virtual string BreakTime { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual Users Users { get; set; }
        [Display(Name = "Lý do làm thêm")]
        public virtual string Reason { get; set; }

        [Display(Name = "Hình thức")]
        public virtual OvertimeType OvertimeType { get; set; }
    }
}
