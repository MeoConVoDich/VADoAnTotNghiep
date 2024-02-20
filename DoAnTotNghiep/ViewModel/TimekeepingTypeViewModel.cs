using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class TimekeepingTypeViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Display(Name = "Mã kiểu công")]
        public virtual string Code { get; set; }

        [Display(Name = "Tên kiểu công")]
        public virtual string Name { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Display(Name = "Ghi chú")]
        public virtual string Note { get; set; }
    }
}
