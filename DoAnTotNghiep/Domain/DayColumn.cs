using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class DayColumn
    {
        public int Day { get; set; }
        public string DayOfWeek { get; set; }
        public string DayField { get; set; }
        public string DayMonth { get; set; }
        public string DateOfWeek { get; set; }
        public DateTime DateTime { get; set; }
        public bool Hidden { get; set; }
        public DayColumn(DateTime dateTime)
        {
            DateTime = dateTime;
            Day = dateTime.Day;
            DayOfWeek = DateTimeExtentions.MapDayOfWeeks[dateTime.DayOfWeek];
            DayField = $"Day{Day:00}";
            DayMonth = $"{dateTime.Day}/{dateTime.Month}";
            DateOfWeek = $"{dateTime.DayOfWeek.GetDayOfWeekByName()} - {dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
        }
    }
}
