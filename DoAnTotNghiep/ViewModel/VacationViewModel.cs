using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class VacationViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }
        public virtual string UsersId { get => UsersData?.Id; set { } }

        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => UsersData?.Name; set { } }

        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => UsersData?.Code; set { } }
        public virtual UsersViewModel UsersData { get; set; } = new UsersViewModel();

        [Display(Name = "Nghỉ từ ngày")]
        public virtual DateTime StartDate { get; set; }

        [Display(Name = "Nghỉ đến ngày")]
        public virtual DateTime EndDate { get; set; }

        [Display(Name = "Tổng ngày nghỉ")]
        public virtual decimal? NumberOfDays { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Lý do không phê duyệt")]
        public virtual string NotApprovedReason { get; set; }

        [Display(Name = "Loại hình nghỉ")]
        public virtual string TimekeepingTypeId { get; set; }

        [Display(Name = "Loại hình nghỉ")]
        public virtual string TimekeepingTypeName { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ca nghỉ")]
        public virtual ChooseBreak ChooseBreak { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = "Ngày kiểm duyệt")]
        public virtual DateTime? ApprovedDate { get; set; }

        [Display(Name = "Đối tượng nhập")]
        public virtual CreatorObject CreatorObject { get; set; }
        
        [Display(Name = "Ngày tạo")]
        public virtual DateTime CreateDate { get; set; }
    }
}
