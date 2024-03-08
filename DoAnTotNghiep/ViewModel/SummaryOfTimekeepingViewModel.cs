using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class SummaryOfTimekeepingViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }
        public virtual string UsersId { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }

        [Display(Name = "Công chuẩn")]
        public virtual int? StandradDay { get; set; }

        [Display(Name = "Số phút đi muộn")]
        public virtual int? WorkLateMinutes { get; set; }

        [Display(Name = "Số phút về sóm")]
        public virtual int? LeaveEarlyMinutes { get; set; }

        [Display(Name = "Số giờ làm thêm")]
        public virtual decimal? OvertimeHour { get; set; }
        public Dictionary<string, string> DataType { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DataFormula { get; set; } = new Dictionary<string, string>();
    }
}
