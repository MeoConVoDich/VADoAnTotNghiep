using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class VacationEditModel : EditBaseModel
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
        [Display(Name = "Nghỉ từ ngày")]
        public virtual DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Nghỉ đến ngày")]
        public virtual DateTime EndDate { get; set; }

        [Display(Name = "Tổng ngày nghỉ")]
        public virtual decimal? NumberOfDays { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Lý do không phê duyệt")]
        public virtual string NotApprovedReason { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Loại hình nghỉ")]
        public virtual string TimekeepingTypeId { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Chọn ca nghỉ")]
        public virtual ChooseBreak ChooseBreak { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = "Ngày phê duyệt")]
        public virtual DateTime? ApprovedDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual CreatorObject CreatorObject { get; set; }
        public virtual UsersEditModel UsersData { get; set; } = new UsersEditModel();

        public Property<VacationEditModel> Property { get; set; } = new Property<VacationEditModel>();

        public VacationEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.ChooseBreak)] = Enum.GetValues(typeof(ChooseBreak)).Cast<ChooseBreak>()
                 .Where(c => c != ChooseBreak.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));

            InputFields.Add<VacationEditModel, ChooseBreak>(c => c.ChooseBreak);
            InputFields.Add<VacationEditModel>(c => c.StartDate);
            InputFields.Add<VacationEditModel>(c => c.EndDate);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.ChooseBreak))
            {
                if (ChooseBreak == ChooseBreak.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartDate))
            {
                if (StartDate >= EndDate)
                {
                    Errors.AddExist(nameProperty, "Nghỉ từ ngày phải bé hơn hoặc bằng nghỉ đến ngày!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndDate))
            {
                if (StartDate >= EndDate)
                {
                    Errors.AddExist(nameProperty, "Nghỉ đến ngày phải lơn hơn hoặc bằng nghỉ từ ngày!");
                }
            }
            return Errors;
        }

        public void TimeCalculator()
        {
            try
            {
                if (EndDate >= StartDate)
                {
                    NumberOfDays = (EndDate - StartDate).Days;
                    if (ChooseBreak != ChooseBreak.All && ChooseBreak != ChooseBreak.FullDayBreak)
                    {
                        NumberOfDays = (decimal?)NumberOfDays / 2;
                    }
                    return;
                }
                else
                {
                    NumberOfDays = 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
