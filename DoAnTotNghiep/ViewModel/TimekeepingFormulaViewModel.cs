using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class TimekeepingFormulaViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Display(Name = "Tên công thức")]
        public virtual string Name { get; set; }

        [Display(Name = "Mã công thức")]
        public virtual string Code { get; set; }

        [Display(Name = "Công thức")]
        public virtual string Formula { get; set; }
    }
}
