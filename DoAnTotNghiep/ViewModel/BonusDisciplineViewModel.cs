using DoAnTotNghiep.Config;
using System.ComponentModel.DataAnnotations;
using System;

namespace DoAnTotNghiep.ViewModel
{
    public class BonusDisciplineViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        public virtual string UsersId { get; set; }

        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get; set; }

        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get; set; }

        [Display(Name = "Tên quyết định")]
        public virtual string Name { get; set; }

        [Display(Name = "Số quyết định")]
        public virtual string Code { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Display(Name = "Loại quyết định")]
        public virtual BonusDisciplineType BonusDisciplineType { get; set; }

        [Display(Name = "Số tiền")]
        public virtual int? Amount { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Ngày hiệu lực")]
        public virtual DateTime Date { get; set; }
    }
}
