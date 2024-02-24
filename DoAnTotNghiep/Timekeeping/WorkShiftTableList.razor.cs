using AntDesign;
using AntDesign.TableModels;
using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
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
        [Inject] CustomNotificationManager Notice { get; set; }
        List<WorkShiftTable> WorkShiftTables = new List<WorkShiftTable>();
        List<TimekeepingShift> TimekeepingShifts = new List<TimekeepingShift>();
        List<WorkShiftTableViewModel> selectedApproveRows = new();
        List<WorkShiftTableViewModel> WorkShiftTableViewModels = new List<WorkShiftTableViewModel>();
        Table<WorkShiftTableViewModel> table;
        WorkShiftTableFilterEditModel workShiftTableFilterModel = new WorkShiftTableFilterEditModel();
        IEnumerable<WorkShiftTableViewModel> selectedRows;
        List<DayColumn> dayColumns = new List<DayColumn>();
        List<string> selectedRowIds = new List<string>();
        bool loading;
        bool detailVisible;
        bool cancelApproveModalVisible;
        bool cancelApproving;
        string cancelApproveReason;
        bool approvedModalVisible;
        bool approving;
        bool disapprovedModalVisible;
        bool disapproving;
        string disapprovedReason;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                workShiftTableFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void BuildNameColumn()
        {

            dayColumns.Clear();
            if (workShiftTableFilterModel.Year.IsNotNullOrEmpty() && workShiftTableFilterModel.Month.IsNotNullOrEmpty())
            {
                var dates = DateTimeExtentions.GetDates(workShiftTableFilterModel.DefaultStartDate.Value, workShiftTableFilterModel.DefaultEndDate.Value);

            }
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

        async Task LoadDataAsync()
        {
            try
            {
                loading = true;
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

        void OnRowClick(RowData<WorkShiftTableViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = WorkShiftTables.FirstOrDefault(c => c.Id == rowData.Data.Id);
                ids = selectedRows != null ? selectedRows.Select(c => c.Id).ToList() : new();
                if (ids.Contains(selectData.Id))
                {
                    ids.Remove(selectData.Id);
                }
                else
                {
                    ids.Add(selectData.Id);
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
                await SearchAsync();

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
                await SearchAsync();
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
                workShiftTableFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                workShiftTableFilterModel.ReadOnly = false;
                workShiftTableFilterModel.DefaultStartDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt(), 1);
                workShiftTableFilterModel.DefaultEndDate = new DateTime(workShiftTableFilterModel.Year.ToInt(), workShiftTableFilterModel.Month.ToInt() + 1, 1).AddDays(-1);
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
                var deleteList = WorkShiftTables.Where(c => selectedRowIds.Contains(c.Id)).ToList();
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

        async Task SetShiftDefaultAsync()
        {
            try
            {
                if (workShiftTableFilterModel.TimekeepingShiftCode.IsNullOrEmpty())
                {
                    Notice.NotiWarning("Chưa chọn ca làm việc");
                    return;
                }
                if (!workShiftTableFilterModel.DefaultStartDate.HasValue || !workShiftTableFilterModel.DefaultEndDate.HasValue)
                {
                    Notice.NotiWarning("Chưa chọn ngày cần xếp ca");
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

        void SetAllShiftCode<T>(T obj, string code, DateTime startDate, DateTime endDate)
        {
            try
            {
                var dayOfPhase = DateTimeExtentions.GetDictionaryDates(startDate, endDate);
                foreach (var day in dayOfPhase)
                {
                    if (timekeepingPhaseTypeEditModel.GetWorkDay(day.Value).IsNotNullOrEmpty())
                    {
                        obj.SetValue(string.Format("Day{0:00}", day.Value.Day), code);
                    }
                    else
                    {
                        obj.SetValue(string.Format("Day{0:00}", day.Value.Day), null);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }

        }
    }
}
