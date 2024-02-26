using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class WorkShiftTableFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public virtual string Year { get; set; } = DateTime.Now.Year.ToString();
        public virtual string Month { get; set; } = DateTime.Now.Month.ToString();
        public virtual string UsersId { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ca làm việc")]
        public virtual string TimekeepingShiftCode { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Thời gian xếp ca")]
        public virtual DateTime? DefaultStartDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        public virtual DateTime? DefaultEndDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Loại nghỉ hàng tuần")]
        public virtual string DayOffInWeekType { get; set; }

        [Display(Name = "Tuần đầu")]
        public virtual string WorkInFirstWeekType { get; set; }

        [Display(Name = "Ngày nghỉ trong tuần")]
        public List<string> DayOffInWeek { get; set; }

        [Display(Name = "Chọn ngày nghỉ")]
        public List<string> DayOff { get; set; }

        public Property<WorkShiftTableFilterEditModel> Property { get; set; } = new Property<WorkShiftTableFilterEditModel>();

        public WorkShiftTableFilterEditModel()
        {
            DataSource[Property.NameProperty(c => c.DayOffInWeek)] = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>()
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(),v.GetDayOfWeekByName()));
            DataSource[Property.NameProperty(c => c.DayOffInWeekType)] = Enum.GetValues(typeof(DayOffInWeekType)).Cast<DayOffInWeekType>()
                  .Where(c => c != Config.DayOffInWeekType.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.WorkInFirstWeekType)] = Enum.GetValues(typeof(WorkInFirstWeekType)).Cast<WorkInFirstWeekType>()
                  .Where(c => c != Config.WorkInFirstWeekType.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.Name(c => c.Year)] = DateTimeExtentions.GetListYears();
            DataSource[Property.Name(c => c.Month)] = DateTimeExtentions.GetListMonths();
            InputFields.Add<WorkShiftTableFilterEditModel>(c => c.DayOffInWeekType);
            InputFields.Add<WorkShiftTableFilterEditModel>(c => c.WorkInFirstWeekType);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.DayOffInWeekType))
            {
                if (DayOffInWeekType == Config.DayOffInWeekType.All.ToString() || DayOffInWeekType.IsNullOrEmpty())
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.WorkInFirstWeekType))
            {
                if (DayOffInWeekType == Config.DayOffInWeekType.TwoWeekInMonth.ToString())
                {
                    if (WorkInFirstWeekType == Config.WorkInFirstWeekType.All.ToString() || WorkInFirstWeekType.IsNullOrEmpty())
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                }
            }

            return Errors;
        }

    }
}
