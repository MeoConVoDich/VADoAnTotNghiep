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

    public enum ApprovalStatus
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Chờ phê duyệt")]
        Pending = 1,
        [Display(Name = "Phê duyệt")]
        Approved = 2,
        [Display(Name = "Không phê duyệt")]
        Disapproved = 3,
        [Display(Name = "Hủy phê duyệt")]
        CanceledApproved = 4,
    }

    public enum ChooseBreak
    {
        [Display(Name = "Tất cả")]
        All,
        [Display(Name = "Nghỉ buổi sáng")]
        MorningBreak,
        [Display(Name = "Nghỉ buổi chiều")]
        AfternoonBreak,
        [Display(Name = "Nghỉ cả ngày")]
        FullDayBreak,
    }

    public enum CreatorObject
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "CBNV")]
        Staff = 1,
        [Display(Name = "CBNS")]
        HRStaff = 2,
    }

    public enum ViolationType
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Quên chấm công cả ngày")]
        ForgotTimekeepingAllDay = 1,
        [Display(Name = "Quên checkin")]
        ForgotCheckin = 2,
        [Display(Name = "Quên checkout")]
        ForgotCheckout = 3,
        [Display(Name = "Xin không chấm công")]
        NoTimekeeping = 4,
        [Display(Name = "Chấm công muộn/sớm do công việc")]
        TimekeepingLateOrEarly = 5,
        [Display(Name = "Khác")]
        Other = 6,
    }
}
