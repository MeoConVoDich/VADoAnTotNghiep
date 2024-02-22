using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public static class DateTimeExtentions
    {
        public static Dictionary<DayOfWeek, string> MapDayOfWeeks = new Dictionary<DayOfWeek, string>()
        {
            { DayOfWeek.Sunday, "CN" },
            { DayOfWeek.Monday, "T2" },
            { DayOfWeek.Tuesday, "T3" },
            { DayOfWeek.Wednesday, "T4" },
            { DayOfWeek.Thursday, "T5" },
            { DayOfWeek.Friday, "T6" },
            { DayOfWeek.Saturday, "T7" },
        };

        public static Dictionary<DayOfWeek, string> MapDayOfWeekFullVNs = new Dictionary<DayOfWeek, string>()
        {
            {DayOfWeek.Sunday, "CHỦ NHẬT" },
            { DayOfWeek.Monday, "THỨ HAI" },
            { DayOfWeek.Tuesday, "THỨ BA" },
            { DayOfWeek.Wednesday, "THỨ TƯ" },
            { DayOfWeek.Thursday, "THỨ NĂM" },
            { DayOfWeek.Friday, "THỨ SÁU" },
            { DayOfWeek.Saturday, "THỨ BẢY" },
        };
        public static DateTime GetDateTimeOnly(this DateTime? dateTime) => dateTime.Value.Date;

        public static DateTime GetEndTimeNow() => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                        .AddDays(1).AddSeconds(-1);

        public static DateTime GetStartTimeBeforeYear(int year) => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                        .AddYears(-year);

        public static DateTime GetStartTimeBeforeDay(int day) => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                       .AddDays(-day);

        public static DateTime GetEndTimeByDate(this DateTime? dateTime) => new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day)
                        .AddDays(1).AddSeconds(-1);

        public static DateTime GetStartTimeBeforeYearByDate(this DateTime? dateTime, int year) => new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day)
                        .AddYears(-year);

        public static DateTime GetTimeWithHHmmMin(this DateTime? dateTime) => new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, dateTime.Value.Hour, dateTime.Value.Minute, 00);
        public static DateTime GetTimeWithHHmmMax(this DateTime? dateTime) => new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, dateTime.Value.Hour, dateTime.Value.Minute, 59);


        public static string ToShortDate(this DateTime? dateTime)
        {
            return dateTime?.ToString("dd/MM/yyyy");
        }

        public static string ToYear(this DateTime? dateTime)
        {
            return dateTime?.ToString("yyyy");
        }

        public static string ToMonthYear(this DateTime? dateTime)
        {
            return dateTime?.ToString("MM/yyyy");
        }

        public static string ToShortDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
        public static string ToTimeOfDay(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }

        public static string ToFullDate(this DateTime? dateTime)
        {
            return dateTime?.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string ToFullDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static Dictionary<string, ISelectItem> GetListYears()
        {
            Dictionary<string, ISelectItem> DicYears = new Dictionary<string, ISelectItem>();
            for (int i = 2022; i < DateTime.Now.Year + 3; i++)
            {
                DicYears.Add(i.ToString(), new SelectItem(i.ToString()));
            }
            return DicYears;
        }

        public static Dictionary<string, ISelectItem> GetListMonths()
        {
            Dictionary<string, ISelectItem> DicMonths = new Dictionary<string, ISelectItem>();
            for (int i = 1; i <= 12; i++)
            {
                DicMonths.Add(i.ToString(), new SelectItem($"Tháng {i}"));
            }
            return DicMonths;
        }

        public static Dictionary<string, ISelectItem> GetListDays()
        {
            Dictionary<string, ISelectItem> DicDays = new Dictionary<string, ISelectItem>();
            DicDays.Add("5", new SelectItem("5"));
            DicDays.Add("10", new SelectItem("10"));
            DicDays.Add("15", new SelectItem("15"));
            DicDays.Add("30", new SelectItem("30"));
            return DicDays;
        }

        public static Dictionary<string, DateTime> GetDictionaryDates(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, DateTime> dates = new Dictionary<string, DateTime>();
            for (DateTime date = startDate.Date; endDate.Date.CompareTo(date) >= 0; date = date.AddDays(1))
            {
                dates.Add(string.Format("Day{0:00}", date.Day), date);
            }
            return dates;
        }

        public static List<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();
            for (DateTime date = startDate.Date; endDate.Date.CompareTo(date) >= 0; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }

        public static int GetDateAmount(DateTime startDate, DateTime endDate)
        {
            return (endDate.Date - startDate.Date).Days + 1;
        }

        public static DateTime GetDateTimeOnly(this DateTime dateTime) => dateTime.Date;

        public static DateTime GetEndTimeByDate(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)
                .AddDays(1).AddSeconds(-1);

        public static string GetSeniorityType(this DateTime? startDate, DateTime? now)
        {
            string seniority = string.Empty;

            if (startDate.HasValue)
            {
                var value = (now ?? DateTime.Now).Subtract(startDate.Value);
                if (value.Days >= 1)
                {
                    DateTime interval = DateTime.MinValue + value;
                    string year = interval.Year != 1 ? string.Format("{0} năm, ", interval.Year - 1) : string.Empty;
                    string month = interval.Month != 1 ? string.Format("{0} tháng, ", interval.Month - 1) : string.Empty;
                    string day = interval.Day != 1 ? string.Format("{0} ngày", interval.Day - 1) : string.Empty;
                    seniority = year + month + day;
                }
            }
            return seniority.TrimEnd(' ', ',');
        }

        public static string ToHourMinute(this decimal hour)
        {
            var interval = TimeSpan.FromHours((double)hour);
            return $"{interval.Days * 24 + interval.Hours} giờ {interval.Minutes} phút";
        }

        public static string MinuteToHourMinute(this double minute)
        {
            var interval = TimeSpan.FromMinutes(minute);
            return $"{interval.Days * 24 + interval.Hours} giờ {interval.Minutes} phút";
        }

        public static DateTime? ConvertToTodayDate(this DateTime? originalDate)
        {
            if (originalDate == null)
            {
                return null;
            }

            DateTime newDate = DateTime.Now.Date.AddSeconds(originalDate.Value.TimeOfDay.TotalSeconds);

            return newDate;
        }

        public static DateTime? ConvertToNextDate(this DateTime? originalDate)
        {
            if (originalDate == null)
            {
                return null;
            }

            DateTime newDate = DateTime.Now.Date.AddDays(1).AddSeconds(originalDate.Value.TimeOfDay.TotalSeconds);

            return newDate;
        }

        public static int GetQuarter(this DateTime date)
        {
            return (date.Month + 2) / 3;
        }

    }
}
