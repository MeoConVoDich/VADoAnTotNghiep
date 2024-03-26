using AntDesign;
using AntDesign.TableModels;
using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using DoAnTotNghiep.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Timekeeping
{
    public partial class WorkShiftTableList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] WorkShiftTableService WorkShiftTableService { get; set; }
        [Inject] TimekeepingShiftService TimekeepingShiftService { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        List<WorkShiftTable> WorkShiftTables = new List<WorkShiftTable>();
        List<TimekeepingShift> TimekeepingShifts = new List<TimekeepingShift>();
        List<WorkShiftTableViewModel> selectedApproveRows = new();
        List<WorkShiftTableViewModel> WorkShiftTableViewModels = new List<WorkShiftTableViewModel>();
        Table<WorkShiftTableViewModel> table;
        WorkShiftTableFilterEditModel workShiftTableFilterModel = new WorkShiftTableFilterEditModel();
        IEnumerable<WorkShiftTableViewModel> selectedRows;
        List<DayColumn> dayColumns = new List<DayColumn>();
        List<DayColumn> dayOffs = new List<DayColumn>();
        List<string> selectedRowIds = new List<string>();
        InputWatcher inputWatcher;
        bool loading;
        string editId;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                workShiftTableFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                workShiftTableFilterModel.ReadOnly = true;
                BuildNameColumn();
                if (PermissionClaim.TIMEKEEPINGSHIFTSTAFF_VIEW)
                {
                    await LoadDataAsync();
                    await LoadTimekeepingShiftsAsync();
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void BuildNameColumn()
        {
            try
            {
                dayColumns.Clear();
                if (workShiftTableFilterModel.Year.IsNotNullOrEmpty() && workShiftTableFilterModel.Month.IsNotNullOrEmpty())
                {
                    var startDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt(), 1);
                    var endDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt() + 1, 1).AddDays(-1);
                    var dates = DateTimeExtentions.GetDates(startDate, endDate);
                    foreach (var date in dates)
                    {
                        dayColumns.Add(new DayColumn(date));
                    }
                    if (dayColumns.Count < 31)
                    {
                        int addDay = 31 - dayColumns.Count;
                        for (int i = 0; i < addDay; i++)
                        {
                            dayColumns.Add(new DayColumn(dayColumns.ElementAt(0).DateTime) { Hidden = true });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        async Task LoadDataAsync()
        {
            try
            {
                loading = true;
                selectedRows = null;
                StateHasChanged();
                var search = Mapper.Map<WorkShiftTableSearch>(workShiftTableFilterModel);
                var page = await WorkShiftTableService.GetPageWithFilterAsync(search);
                WorkShiftTables = page.Item1 ?? new List<WorkShiftTable>();
                WorkShiftTableViewModels = Mapper.Map<List<WorkShiftTableViewModel>>(WorkShiftTables ?? new List<WorkShiftTable>());
                int stt = workShiftTableFilterModel.Page.PageSize * (workShiftTableFilterModel.Page.PageIndex - 1) + 1;
                WorkShiftTableViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                workShiftTableFilterModel.Page.Total = page.Item2;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                loading = false;
                StateHasChanged();
            }
        }

        async Task LoadTimekeepingShiftsAsync()
        {
            try
            {
                StateHasChanged();
                var data = await TimekeepingShiftService.GetAllWithFilterAsync(new TimekeepingShiftSearch() { EffectiveState = EffectiveState.Active });
                TimekeepingShifts = data.Item1 ?? new List<TimekeepingShift>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnRowClick(RowData<WorkShiftTableViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = WorkShiftTables.FirstOrDefault(c => c.UsersId == rowData.Data.UsersId);
                ids = selectedRows != null ? selectedRows.Select(c => c.UsersId).ToList() : new();
                if (ids.Contains(selectData.UsersId))
                {
                    ids.Remove(selectData.UsersId);
                }
                else
                {
                    ids.Add(selectData.UsersId);
                }
                table.SetSelection(ids.ToArray());
                selectedRowIds = ids;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                if (e.Page > 0)
                {
                    workShiftTableFilterModel.Page.PageIndex = e.Page;
                }
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageSizeChangeAsync(PaginationEventArgs e)
        {
            try
            {
                workShiftTableFilterModel.Page.PageIndex = 1;
                workShiftTableFilterModel.Page.PageSize = e.PageSize;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchAsync()
        {
            try
            {
                workShiftTableFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task YearChangeAsync(string year)
        {
            try
            {
                workShiftTableFilterModel.Year = year;
                BuildNameColumn();
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchMonthChangedAsync(string month)
        {
            try
            {
                workShiftTableFilterModel.Month = month;
                BuildNameColumn();
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit()
        {
            try
            {
                dayOffs.Clear();
                workShiftTableFilterModel.ReadOnly = false;
                workShiftTableFilterModel.DefaultStartDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt(), 1);
                workShiftTableFilterModel.DefaultEndDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt() + 1, 1).AddDays(-1);
                var dates = DateTimeExtentions.GetDates(workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                foreach (var date in dates)
                {
                    dayOffs.Add(new DayColumn(date));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync()
        {
            try
            {
                bool result = new();
                if (selectedRows.Any() != true)
                {
                    Notice.NotiError("Không có nhân viên được chọn!");
                    return;
                }
                var deleteList = WorkShiftTables.Where(c => selectedRowIds.Contains(c.UsersId) && c.Id.IsNotNullOrEmpty()).ToList();
                result = await WorkShiftTableService.DeleteListAsync(deleteList);
                if (result)
                {
                    Notice.NotiSuccess("Xoá dữ liệu thành công!");
                    await LoadDataAsync();
                    
                }
                else
                {
                    Notice.NotiError("Xoá dữ liệu thất bại!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SaveAsync()
        {
            try
            {
                var updateData = WorkShiftTables.Where(c => c.Id.IsNotNullOrEmpty()).ToList();
                var addData = WorkShiftTables.Where(c => c.Id.IsNullOrEmpty()).ToList();
                addData.ForEach(c => c.Id = ObjectExtentions.GenerateGuid());

                var result = await WorkShiftTableService.AddUpdateListAsync(addData, updateData);
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    workShiftTableFilterModel.ReadOnly = true;
                    await LoadDataAsync();
                }
                else
                {
                    Notice.NotiError("Cập nhật dữ liệu thất bại!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task CancelAsync()
        {
            try
            {
                workShiftTableFilterModel.ReadOnly = true;
                await LoadDataAsync();
                workShiftTableFilterModel.DefaultStartDate = null;
                workShiftTableFilterModel.DefaultEndDate = null;
                workShiftTableFilterModel.TimekeepingShiftCode = null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void SetShiftDefault()
        {
            try
            {
                var errorMessageStore = workShiftTableFilterModel.ValidateAll();
                if (!inputWatcher.Validate() || errorMessageStore.Any())
                {
                    if (errorMessageStore.Any())
                    {
                        inputWatcher.NotifyFieldChanged(errorMessageStore.First().Key, errorMessageStore);
                    }
                    Notice.NotiWarning("Dữ liệu còn thiếu hoặc không hợp lệ!");
                    return;
                }
                loading = true;
                List<WorkShiftTableViewModel> shiftViews = new();
                List<WorkShiftTable> shiftDatas = new();
                if (selectedRows?.Any() == true)
                {
                    shiftViews = selectedRows.ToList();
                    var selectedIds = shiftViews.Select(c => c.UsersId).ToList();
                    shiftDatas = WorkShiftTables.Where(c => selectedIds.Contains(c.UsersId)).ToList();
                    shiftViews.ForEach(c =>
                    {
                        if (c.UsersId != null)
                        {
                            SetAllShiftCode(c, workShiftTableFilterModel.TimekeepingShiftCode, workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                        }
                    });
                    shiftDatas.ForEach(c =>
                    {
                        if (c.UsersId != null)
                        {
                            SetAllShiftCode(c, workShiftTableFilterModel.TimekeepingShiftCode, workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                        }
                    });
                }
                else
                {
                    WorkShiftTableViewModels.ForEach(c =>
                    {
                        if (c.UsersId != null)
                        {
                            SetAllShiftCode(c, workShiftTableFilterModel.TimekeepingShiftCode, workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                        }
                    });
                    WorkShiftTables.ForEach(c =>
                    {
                        if (c.UsersId != null)
                        {
                            SetAllShiftCode(c, workShiftTableFilterModel.TimekeepingShiftCode, workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                loading = false;
            }
        }

        void SetDate(DateTime?[] dateTimes)
        {
            try
            {
                dayOffs.Clear();
                workShiftTableFilterModel.DefaultStartDate = dateTimes[0];
                workShiftTableFilterModel.DefaultEndDate = dateTimes[1];
                if (workShiftTableFilterModel.DefaultStartDate.HasValue && workShiftTableFilterModel.DefaultEndDate.HasValue)
                {
                    var dates = DateTimeExtentions.GetDates(workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);
                    foreach (var date in dates)
                    {
                        dayOffs.Add(new DayColumn(date));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void SetAllShiftCode<T>(T obj, string code, DateTime startDate, DateTime endDate)
        {
            try
            {
                var dayOfPhase = DateTimeExtentions.GetDictionaryDates(startDate, endDate);
                var Saturdays = DateTimeExtentions.GetDates(startDate, endDate)
                                  .Where(dateTime => dateTime.DayOfWeek == DayOfWeek.Saturday)
                                  .ToList();
                foreach (var day in dayOfPhase)
                {
                    if (GetOffDay(day.Value, Saturdays))
                    {
                        obj.SetValue(string.Format("Day{0:00}", day.Value.Day), null);
                    }
                    else
                    {
                        obj.SetValue(string.Format("Day{0:00}", day.Value.Day), code);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        bool GetOffDay(DateTime dateTime, List<DateTime> saturdays)
        {
            try
            {
                if (workShiftTableFilterModel.DayOffInWeekType == DayOffInWeekType.Sun.ToString())
                {
                    if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return true;
                    }
                }
                else if (workShiftTableFilterModel.DayOffInWeekType == DayOffInWeekType.SatAndSun.ToString())
                {
                    if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        return true;
                    }
                }
                else if (workShiftTableFilterModel.DayOffInWeekType == DayOffInWeekType.TwoWeekInMonth.ToString())
                {
                    if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return true;
                    }
                    else if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        List<int> indexes = new();
                        
                        if (workShiftTableFilterModel.WorkInFirstWeekType == WorkInFirstWeekType.OffFirstWeek.ToString())
                        {
                            indexes = new List<int>() { 0, 2, 4, 6 };
                        }
                        else if (workShiftTableFilterModel.WorkInFirstWeekType == WorkInFirstWeekType.WorkFirstWeek.ToString())
                        {
                            indexes = new List<int>() { 1, 3, 5, 7 };
                        }
                        var isWorkDay = saturdays.Where((c, index) => c.Date == dateTime.Date && indexes.Contains(index)).Any();
                        if (isWorkDay)
                        {
                            return true;
                        }
                    }
                }
                else if (workShiftTableFilterModel.DayOffInWeekType == DayOffInWeekType.InWeek.ToString())
                {
                    if (workShiftTableFilterModel.DayOffInWeek?.Any() == true)
                    {
                        if (workShiftTableFilterModel.DayOffInWeek.Contains(dateTime.DayOfWeek.ToString()))
                        {
                            return true;
                        }
                    }
                }
                else if (workShiftTableFilterModel.DayOffInWeekType == DayOffInWeekType.Custom.ToString())
                {
                    if (workShiftTableFilterModel.DayOff?.Any() == true)
                    {
                        if (workShiftTableFilterModel.DayOff.Contains(dateTime.Date.ToString()))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void SetShift(WorkShiftTableViewModel model, string propertyName, string shiftCode)
        {

            try
            {
                model.SetValue(propertyName, shiftCode);
                var selectModel = WorkShiftTables.FirstOrDefault(c => c.Id == model.Id);
                if (selectModel == null)
                {
                    return;
                }
                selectModel.SetValue(propertyName, shiftCode);
                if (shiftCode.IsNullOrEmpty())
                {
                    editId = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        void StartEdit(string id)
        {
            editId = id;
        }

        void StopEdit()
        {
            editId = null;
        }
    }
}
