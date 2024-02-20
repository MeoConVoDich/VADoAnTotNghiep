using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class TimekeepingShiftViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Display(Name = "Mã ca")]
        public virtual string Code { get; set; }

        [Display(Name = "Tên ca")]
        public virtual string Name { get; set; }

        [Display(Name = "Thời gian bắt đầu")]
        public virtual DateTime? StartTime { get; set; }

        [Display(Name = "Thời gian kết thúc")]
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }
    }
}
