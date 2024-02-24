using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingExplanationEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        public virtual string UsersId { get => UsersData?.Id; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => UsersData?.Name; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => UsersData?.Code; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ngày giải trình")]
        public virtual DateTime? RegisterDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Giờ vào")]
        public virtual DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Giờ ra")]
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Lý do không phê duyệt")]
        public virtual string DisapprovedReason { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Kiểu công")]
        public virtual string TimekeepingTypeId { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Loại vi phạm")]
        public virtual ViolationType ViolationType { get; set; }

        [Display(Name = "Ngày phê duyệt")]
        public virtual DateTime? ApprovedDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual CreatorObject CreatorObject { get; set; }
        public virtual UsersEditModel UsersData { get; set; } = new UsersEditModel();

        public Property<TimekeepingExplanationEditModel> Property { get; set; } = new Property<TimekeepingExplanationEditModel>();

        public TimekeepingExplanationEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.ViolationType)] = Enum.GetValues(typeof(ViolationType)).Cast<ViolationType>()
              .Where(c => c != ViolationType.All).OrderBy(c => c)
             .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            InputFields.Add<TimekeepingExplanationEditModel, ViolationType>(c => c.ViolationType);
            InputFields.Add<TimekeepingExplanationEditModel>(c => c.StartTime);
            InputFields.Add<TimekeepingExplanationEditModel>(c => c.EndTime);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.ViolationType))
            {
                if (ViolationType == ViolationType.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartTime))
            {
                if (StartTime >= EndTime && EndTime.HasValue && StartTime.HasValue)
                {
                    Errors.AddExist(nameProperty, "Giờ vào phải bé hơn giờ ra!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndTime))
            {
                if (StartTime >= EndTime && EndTime.HasValue && StartTime.HasValue)
                {
                    Errors.AddExist(nameProperty, "Giờ ra phải lơn hơn giờ vào!");
                }
            }
            return Errors;
        }
    }
}
