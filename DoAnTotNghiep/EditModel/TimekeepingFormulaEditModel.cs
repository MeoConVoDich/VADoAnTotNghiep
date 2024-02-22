using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingFormulaEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên công thức")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã công thức")]
        public virtual string Code { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Công thức")]
        public virtual string Formula { get; set; }
        public virtual int CountCode { get; set; }
        public virtual DateTime CreateDate { get; set; }

    }
}
