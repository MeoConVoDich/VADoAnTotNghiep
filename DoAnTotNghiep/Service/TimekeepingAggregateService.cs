using AutoMapper;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.SearchModel;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Service
{
    public class TimekeepingAggregateService
    {
        private readonly ISessionFactory _session;
        readonly IMapper _mapper;
        public TimekeepingAggregateService(ISessionFactory session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        IQueryable<TimekeepingAggregate> CreateFilter(TimekeepingAggregateSearch model, ISession session)
        {
            var query = session.Query<TimekeepingAggregate>();
            if (model.UsersId.IsNotNullOrEmpty())
            {
                query = query.Where(c => c.UsersId == model.UsersId);
            }
            if (model.UsersIds?.Any() == true)
            {
                query = query.Where(c => model.UsersId.Contains(c.UsersId));
            }
            if (model.Year.HasValue)
            {
                query = query.Where(c => c.Year == model.Year);
            }
            if (model.Month.HasValue)
            {
                query = query.Where(c => c.Month == model.Month);
            }
            return query;
        }

        public async Task<(List<TimekeepingAggregate>, int)> GetAllWithFilterAsync(TimekeepingAggregateSearch search)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var filter = CreateFilter(search, session);
                        var data = await filter.OrderBy(c => c.DateOfPhase).ToListAsync();
                        var count = await filter.CountAsync();
                        transaction.Commit();
                        return (data, count);
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        return (new List<TimekeepingAggregate>(), 0);
                        throw;
                    }
                }
            }
        }

        public async Task<bool> AggregateAsync(TimekeepingAggregateSearch model)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TimekeepingAggregate> timekeepingAggregateAdds = new List<TimekeepingAggregate>();
                        List<string> usersIds = new List<string>();
                        if (model.UsersIds?.Any() == true)
                        {
                            usersIds = model.UsersIds;
                        }
                        else
                        {
                            var filterUsers = session.Query<Users>();
                            if (model.CodeOrName.IsNotNullOrEmpty())
                            {
                                filterUsers = filterUsers.Where(c => c.Code.Contains(model.CodeOrName) || c.Name.Contains(model.CodeOrName));
                            }
                            var usersData = await filterUsers.ToListAsync();
                            usersIds = usersData.Select(c => c.Id).ToList();
                        }
                        var fromDate = new DateTime(model.Year.Value, model.Month.Value, 1);
                        var toDate = new DateTime(model.Year.Value, model.Month.Value + 1, 1).AddDays(-1);

                        var dayOfPhase = DateTimeExtentions.GetDictionaryDates(fromDate, toDate);
                        var timekeepingTypes = await session.Query<TimekeepingType>()
                               .Where(c => c.EffectiveState == EffectiveState.Active).ToListAsync();
                        var timekeepingShifts = await session.Query<TimekeepingShift>()
                                .Where(c => c.EffectiveState == EffectiveState.Active).ToListAsync();
                        int j = 0;
                        int batchSize = 200;

                        while (usersIds.Count >= j * batchSize)
                        {
                            var usersBatchIds = usersIds.Skip(j * batchSize).Take(batchSize).ToList();
                            j++;
                            await session.Query<TimekeepingAggregate>()
                             .Where(c => c.Year == model.Year)
                             .Where(c => c.Month == model.Month)
                             .Where(c => usersBatchIds.Contains(c.UsersId))
                             .DeleteAsync();
                            var workShiftTables = await session.Query<WorkShiftTable>()
                                .Where(c => c.Year == model.Year)
                                .Where(c => c.Month == model.Month)
                                .Where(c => usersBatchIds.Contains(c.UsersId))
                                .ToListAsync();
                            var vacations = await session.Query<Vacation>()
                                .Where(c => c.ApprovalStatus == ApprovalStatus.Approved)
                                .Where(c => c.StartDate.Value.Year == model.Year && c.StartDate.Value.Month == model.Month)
                                .Where(c => c.EndDate.Value.Year == model.Year && c.EndDate.Value.Month == model.Month)
                                .Where(c => usersBatchIds.Contains(c.Users.Id))
                                .OrderByDescending(c => c.CreateDate).ToListAsync();
                            var timekeepingExplanations = await session.Query<TimekeepingExplanation>()
                               .Where(c => c.ApprovalStatus == ApprovalStatus.Approved)
                               .Where(c => c.RegisterDate.Value.Year == model.Year && c.RegisterDate.Value.Month == model.Month)
                               .Where(c => usersBatchIds.Contains(c.Users.Id))
                               .OrderByDescending(c => c.CreateDate).ToListAsync();
                            var fingerprintManagements = await session.Query<FingerprintManagement>()
                               .Where(c => c.Year == model.Year)
                                .Where(c => c.Month == model.Month)
                                .Where(c => usersBatchIds.Contains(c.UsersId))
                                .ToListAsync();

                            foreach (var usersId in usersBatchIds)
                            {
                                foreach (var day in dayOfPhase)
                                {
                                    TimekeepingAggregate dt;
                                    var fingerprintManagementDays = fingerprintManagements
                                        .Where(c => c.UsersId == usersId)
                                        .Where(c => c.ScanDate.Value.Date == day.Value.Date)
                                        .OrderBy(c => c.ScanDate)
                                        .ToList();
                                    var timekeepingExplanation = timekeepingExplanations
                                        .Where(c => c.RegisterDate.Value.Date == day.Value.Date)
                                        .Where(c => c.UsersData.Id == usersId)
                                        .OrderByDescending(c => c.ApprovedDate).FirstOrDefault();
                                    var vacation = vacations
                                        .Where(c => c.StartDate.Value.Date <= day.Value.Date)
                                        .Where(c => c.EndDate.Value.Date >= day.Value.Date)
                                        .Where(c => c.UsersData.Id == usersId)
                                        .OrderByDescending(c => c.ApprovedDate).FirstOrDefault();
                                    dt = new TimekeepingAggregate()
                                    {
                                        Id = ObjectExtentions.GenerateGuid(),
                                        UsersId = usersId,
                                        DateOfPhase = day.Value,
                                        Month = model.Month.Value,
                                        Year = model.Year.Value,
                                    };
                                    var shiftStaff = workShiftTables.FirstOrDefault(c => c.UsersId == usersId);
                                    var shiftCodeByDay = GetDataFromShiftStaff(shiftStaff, day.Key);
                                    var shift = timekeepingShifts.FirstOrDefault(c => c.Code == shiftCodeByDay);
                                    if (shift != null)
                                    {
                                        dt.ShiftOfPhase = shiftCodeByDay;
                                    }
                                    else
                                    {
                                        dt.ShiftOfPhase = null;
                                    }
                                    bool isFullCheck = false;
                                    bool shiftHasBreakTime = false;
                                    if (timekeepingExplanation != null)
                                    {
                                        dt.CheckinDate = dt.DateOfPhase.Value.AddTicks(timekeepingExplanation.StartTime.Value.TimeOfDay.Ticks);
                                        dt.CheckoutDate = dt.DateOfPhase.Value.AddTicks(timekeepingExplanation.EndTime.Value.TimeOfDay.Ticks);
                                        dt.TimekeepingCode = timekeepingTypes?.FirstOrDefault(c => c.Id == timekeepingExplanation.TimekeepingTypeId)?.Code;
                                        shiftHasBreakTime = shift != null && shift.BreaksTimeType == BreaksTimeType.Has;
                                        isFullCheck = true;
                                    }
                                    else
                                    {
                                        if (fingerprintManagementDays.Count > 0)
                                        {
                                            dt.CheckinDate = fingerprintManagementDays.FirstOrDefault().ScanDate;
                                        }
                                        if (fingerprintManagementDays.Count > 1)
                                        {
                                            dt.CheckoutDate = fingerprintManagementDays.LastOrDefault().ScanDate;
                                        }
                                        isFullCheck = dt.CheckinDate.HasValue && dt.CheckoutDate.HasValue;
                                        shiftHasBreakTime = shift != null && shift.BreaksTimeType == BreaksTimeType.Has;
                                        if (shift != null)
                                        {
                                            if (isFullCheck)
                                            {
                                                if (shiftHasBreakTime)
                                                {
                                                    if (dt.HasFullDay(shift))
                                                    {
                                                        dt.TimekeepingCode = shift.TimekeepingTypeFull ?? shift.TimekeepingTypeOff;
                                                    }
                                                    else if (dt.HasFirstHaflDay(shift))
                                                    {
                                                        dt.TimekeepingCode = shift.TimekeepingTypeFirst ?? shift.TimekeepingTypeOff;
                                                    }
                                                    else if (dt.HasLastHaflDay(shift))
                                                    {
                                                        dt.TimekeepingCode = shift.TimekeepingTypeSecond ?? shift.TimekeepingTypeOff;
                                                    }
                                                    else
                                                    {
                                                        dt.TimekeepingCode = shift.TimekeepingTypeOff;
                                                    }
                                                }
                                                else
                                                {
                                                    dt.TimekeepingCode = shift.TimekeepingTypeFull ?? shift.TimekeepingTypeOff;
                                                }
                                            }
                                            else
                                            {
                                                dt.TimekeepingCode = shift.TimekeepingTypeOff;
                                            }
                                        }
                                        else
                                        {
                                            dt.TimekeepingCode = TimekeepingTypeDefault.OFF.ToString();
                                        }
                                    }
                                    if (timekeepingExplanation == null)
                                    {
                                        if (vacation != null)
                                        {
                                            dt.TimekeepingCode = timekeepingTypes.FirstOrDefault(c => c.Id == vacation.TimekeepingTypeId)?.Code;
                                        }
                                    }

                                    if (shift != null && isFullCheck)
                                    {
                                        TimeSpan late;
                                        TimeSpan early;
                                        TimeSpan applyCheckin = shift.StartTime.TimeOfDay;
                                        TimeSpan applyCheckout = shift.EndTime.TimeOfDay;
                                        if (vacation != null && vacation.ChooseBreak == ChooseBreak.FullDayBreak)
                                        {
                                            dt.WorkLateMinutes = 0;
                                            dt.LeaveEarlyMinutes = 0;
                                        }
                                        if (shiftHasBreakTime)
                                        {
                                            if (vacation != null)
                                            {
                                                if (vacation.ChooseBreak == ChooseBreak.MorningBreak)
                                                {
                                                    applyCheckin = shift.EndBreaksTime.Value.TimeOfDay;
                                                }
                                                else if (vacation.ChooseBreak == ChooseBreak.AfternoonBreak)
                                                {
                                                    applyCheckout = shift.StartBreaksTime.Value.TimeOfDay;
                                                }
                                            }
                                            else
                                            {
                                                if (dt.CheckinDate.Value.TimeOfDay >= shift.StartBreaksTime.Value.TimeOfDay)
                                                {
                                                    applyCheckin = shift.EndBreaksTime.Value.TimeOfDay;
                                                }
                                                else if (dt.CheckoutDate.Value.TimeOfDay <= shift.EndBreaksTime.Value.TimeOfDay)
                                                {
                                                    applyCheckout = shift.StartBreaksTime.Value.TimeOfDay;
                                                }
                                            }
                                        }
                                        late = applyCheckin.Subtract(dt.CheckinDate.Value.TimeOfDay);
                                        early = applyCheckout.Subtract(dt.CheckoutDate.Value.TimeOfDay);

                                        if (late.TotalMinutes >= 0)
                                        {
                                            dt.WorkLateMinutes = 0;
                                        }
                                        else
                                        {
                                            dt.WorkLateMinutes = Convert.ToInt32(-late.TotalMinutes);
                                        }
                                        if (early.TotalMinutes <= 0)
                                        {
                                            dt.LeaveEarlyMinutes = 0;
                                        }
                                        else
                                        {
                                            dt.LeaveEarlyMinutes = Convert.ToInt32(early.TotalMinutes);
                                        }
                                    }
                                    timekeepingAggregateAdds.Add(dt);
                                }
                            }
                        }
                        foreach (var item in timekeepingAggregateAdds)
                        {
                            await session.SaveAsync(item);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        string GetDataFromShiftStaff(WorkShiftTable source, string dateNumber)
        {
            return source?.GetValue(dateNumber)?.ToString() ?? string.Empty;
        }
    }
}
