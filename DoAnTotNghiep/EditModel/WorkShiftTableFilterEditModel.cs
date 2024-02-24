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
        public virtual string TimekeepingShiftCode { get; set; }
        public List<string> DayOff{ get; set; }
        public virtual DateTime? DefaultStartDate { get; set; }
        public virtual DateTime? DefaultEndDate { get; set; }

        public Property<WorkShiftTableFilterEditModel> Property { get; set; } = new Property<WorkShiftTableFilterEditModel>();

        public WorkShiftTableFilterEditModel()
        {
            DataSource[Property.NameProperty(c => c.DayOff)] = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>()
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(),v.GetDayOfWeekByName()));
            DataSource[Property.Name(c => c.Year)] = DateTimeExtentions.GetListYears();
            DataSource[Property.Name(c => c.Month)] = DateTimeExtentions.GetListMonths();
        }

        public void SetDate(DateTime?[] dateTimes)
        {
            DefaultStartDate = dateTimes[0];
            DefaultEndDate = dateTimes[1];
        }

        void LoadDay()
        {
            SaturdayAndSundays.Clear();
            if (StartDate.HasValue && EndDate.HasValue)
            {
                SaturdayAndSundays = DateTimeExtentions.GetDates(StartDate.Value, EndDate.Value)
                                  .Where(dateTime => (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday))
                                  .ToList();
            }
        }
    }
}
