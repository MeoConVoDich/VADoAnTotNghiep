using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Config;

namespace DoAnTotNghiep.Config
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 0,
        [Display(Name = "Nữ")]
        Female = 1,
        [Display(Name = "Khác")]
        Other = 2,
        [Display(Name = "Tất cả")]
        All = 3,
    }
}
