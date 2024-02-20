using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class OvertimeRateViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Display(Name = "Ngày hiệu lực")]
        public virtual DateTime Date { get; set; }

        [Display(Name = "Ngày (%)")]
        public virtual int Day { get; set; }

        [Display(Name = "Đêm (%)")]
        public virtual int Night { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }
    }
}
