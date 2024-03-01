using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public static class TimekeepingAggregateExtensions
    {
        public static bool HasFullDay(this TimekeepingAggregate time, TimekeepingShift shift)
        {
            if (!time.CheckinDate.HasValue || !time.CheckoutDate.HasValue
                || !shift.StartBreaksTime.HasValue || !shift.EndBreaksTime.HasValue)
            {
                return false;
            }
            if (time.CheckinDate.Value.TimeOfDay < shift.StartBreaksTime.Value.TimeOfDay
                && time.CheckoutDate.Value.TimeOfDay > shift.EndBreaksTime.Value.TimeOfDay)
            {
                return true;
            }
            return false;
        }

        public static bool HasFirstHaflDay(this TimekeepingAggregate time, TimekeepingShift shift)
        {
            if (!time.CheckinDate.HasValue || !time.CheckoutDate.HasValue
                || !shift.StartBreaksTime.HasValue || !shift.EndBreaksTime.HasValue)
            {
                return false;
            }
            if (time.CheckinDate.Value.TimeOfDay < shift.StartBreaksTime.Value.TimeOfDay
                && time.CheckoutDate.Value.TimeOfDay <= shift.EndBreaksTime.Value.TimeOfDay)
            {
                return true;
            }
            return false;
        }

        public static bool HasLastHaflDay(this TimekeepingAggregate time, TimekeepingShift shift)
        {
            if (!time.CheckinDate.HasValue || !time.CheckoutDate.HasValue
                || !shift.StartBreaksTime.HasValue || !shift.EndBreaksTime.HasValue)
            {
                return false;
            }
            if (time.CheckinDate.Value.TimeOfDay >= shift.StartBreaksTime.Value.TimeOfDay
                && time.CheckoutDate.Value.TimeOfDay > shift.EndBreaksTime.Value.TimeOfDay)
            {
                return true;
            }
            return false;
        }
    }
}
