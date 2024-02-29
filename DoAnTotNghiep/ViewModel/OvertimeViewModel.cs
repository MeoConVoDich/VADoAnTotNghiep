using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class OvertimeViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }
        public virtual string UsersId { get => UsersData?.Id; set { } }

        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => UsersData?.Name; set { } }

        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => UsersData?.Code; set { } }
        public virtual UsersViewModel UsersData { get; set; } = new UsersViewModel();

        [Display(Name = "Ngày làm thêm")]
        public virtual DateTime? RegisterDate { get; set; }

        [Display(Name = "Bắt đầu")]
        public virtual DateTime? StartTime { get; set; }

        [Display(Name = "Kết thúc")]
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Tổng giờ")]
        public virtual decimal? TotalHour { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Lý do không phê duyệt")]
        public virtual string NotApprovedReason { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = "Ngày kiểm duyệt")]
        public virtual DateTime? ApprovedDate { get; set; }

        [Display(Name = "Đối tượng nhập")]
        public virtual CreatorObject CreatorObject { get; set; }

        [Display(Name = "Hình thức")]
        public virtual OvertimeType OvertimeType { get; set; }

        [Display(Name = "Ngày tạo")]
        public virtual DateTime CreateDate { get; set; }
    }
}
