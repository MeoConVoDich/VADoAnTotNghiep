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

    public enum StaffProfileViewMode
    {
        Manager,
        Staff
    }

    public enum BonusDisciplineType
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Khen thưởng")]
        Bonus = 1,
        [Display(Name = "Kỷ luật")]
        Discipline = 2
    }

    public enum EffectiveState
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Không hiệu lực")]
        Disabled = 1,
        [Display(Name = "Hiệu lực")]
        Active = 2,
    }

    public enum BreaksTimeType
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Không nghỉ giữa ca")]
        None = 1,
        [Display(Name = "Nghỉ giữa ca")]
        Has = 2,
    }
}
