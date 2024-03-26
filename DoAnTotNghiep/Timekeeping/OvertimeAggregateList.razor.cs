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
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Timekeeping
{
    public partial class OvertimeAggregateList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] OvertimeAggregateService OvertimeAggregateService { get; set; }
        [Inject] ModalService ModalService { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        OvertimeAggregateFilterEditModel overtimeAggregateFilterModel = new OvertimeAggregateFilterEditModel();
        UsersSearch usersSearch = new UsersSearch();
        List<Users> ListUsers = new List<Users>();
        List<UsersViewModel> UsersViewModels = new List<UsersViewModel>();
        List<OvertimeAggregate> OvertimeAggregates = new List<OvertimeAggregate>();
        List<OvertimeAggregateViewModel> OvertimeAggregateViewModels = new List<OvertimeAggregateViewModel>();
        List<OvertimeAggregate> OvertimeAggregateExcelModels = new List<OvertimeAggregate>();
        OvertimeAggregateViewModel summary = new OvertimeAggregateViewModel();
        IEnumerable<UsersViewModel> selectedRows;
        ButtonProps cancelButtonProps;
        Table<UsersViewModel> table;
        bool usersLoading;
        bool loading;
        bool aggregating;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                cancelButtonProps = new ButtonProps
                {
                    Disabled = false
                };
                usersSearch.Page = new Page() { PageIndex = 1, PageSize = 15 };
                overtimeAggregateFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                if (PermissionClaim.OVERTIMEAGGREGATE_VIEW)
                {
                    await LoadUsersAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadUsersAsync()
        {
            try
            {
                usersLoading = true;
                selectedRows = null;
                StateHasChanged();
                var page = await UsersService.GetPageWithFilterAsync(usersSearch);
                ListUsers = page.Item1 ?? new List<Users>();
                UsersViewModels = Mapper.Map<List<UsersViewModel>>(ListUsers ?? new List<Users>());
                int stt = usersSearch.Page.PageSize * (usersSearch.Page.PageIndex - 1) + 1;
                UsersViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                usersSearch.Page.Total = page.Item2;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                usersLoading = false;
                StateHasChanged();
            }
        }

        async Task LoadDataAsync()
        {
            try
            {
                loading = true;
                StateHasChanged();
                summary = new OvertimeAggregateViewModel();
                var search = Mapper.Map<OvertimeAggregateSearch>(overtimeAggregateFilterModel);
                var page = await OvertimeAggregateService.GetAllWithFilterAsync(search);
                OvertimeAggregates = page.Item1;
                overtimeAggregateFilterModel.Page.Total = page.Item2;
                GetPageData();
                summary.DayHourAmount = OvertimeAggregates.Sum(c => c.DayHourAmount);
                summary.DayHourCoefficientAmount = OvertimeAggregates.Sum(c => c.DayHourCoefficientAmount);
                summary.NightHourAmount = OvertimeAggregates.Sum(c => c.NightHourAmount);
                summary.NightHourCoefficientAmount = OvertimeAggregates.Sum(c => c.NightHourCoefficientAmount);
                summary.Total = OvertimeAggregates.Sum(c => c.Total);
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

        void GetPageData()
        {
            try
            {
                var data = OvertimeAggregates.Skip(overtimeAggregateFilterModel.Page.PageSize * (overtimeAggregateFilterModel.Page.PageIndex - 1))
                    .Take(overtimeAggregateFilterModel.Page.PageSize).ToList();
                OvertimeAggregateViewModels = Mapper.Map<List<OvertimeAggregateViewModel>>(data ?? new List<OvertimeAggregate>());
                int stt = overtimeAggregateFilterModel.Page.PageSize * (overtimeAggregateFilterModel.Page.PageIndex - 1) + 1;
                OvertimeAggregateViewModels.ForEach(c =>
                {
                    if (c.StartTime.HasValue && c.EndTime.HasValue)
                    {
                        c.WorkTime = $"{c.StartTime?.ToString("HH:mm")} - {c.EndTime?.ToString("HH:mm")}";
                    }
                    if (c.StartBreakTime.HasValue && c.EndBreakTime.HasValue)
                    {
                        c.BreakTime = $"{c.StartBreakTime?.ToString("HH:mm")} - {c.EndBreakTime?.ToString("HH:mm")}";
                    }
                    c.Stt = stt++;
                });
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
                usersSearch.Page.PageIndex = 1;
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchDataAsync()
        {
            try
            {
                if (overtimeAggregateFilterModel.UsersId.IsNotNullOrEmpty())
                {
                    await LoadDataAsync();
                }
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
                overtimeAggregateFilterModel.Year = year;
                await SearchDataAsync();
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
                overtimeAggregateFilterModel.Month = month;
                await SearchDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageIndexStaffChangeAsync(PaginationEventArgs e)
        {
            try
            {
                usersSearch.Page.PageIndex = e.Page;
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageSizeStaffChangeAsync(PaginationEventArgs e)
        {
            try
            {
                usersSearch.Page.PageIndex = 1;
                usersSearch.Page.PageSize = e.PageSize;
                await LoadUsersAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void PageIndexChange(PaginationEventArgs e)
        {
            try
            {
                overtimeAggregateFilterModel.Page.PageIndex = e.Page;
                GetPageData();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void PageSizeChange(PaginationEventArgs e)
        {
            try
            {
                overtimeAggregateFilterModel.Page.PageIndex = 1;
                overtimeAggregateFilterModel.Page.PageSize = e.PageSize;
                GetPageData();  
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task OnRowClickAsync(RowData<UsersViewModel> rowData)
        {
            try
            {
                if (overtimeAggregateFilterModel.Year.IsNotNullOrEmpty() && overtimeAggregateFilterModel.Month.IsNotNullOrEmpty() && rowData.IsNotNullOrEmpty())
                {
                    overtimeAggregateFilterModel.UsersId = rowData.Data.Id;
                    await LoadDataAsync();
                }
                else
                {
                    Notice.NotiWarning("Chưa chọn năm, tháng!");
                }
                List<string> ids;
                var selectData = ListUsers.FirstOrDefault(c => c.Id == rowData.Data.Id);
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task AggregationAsync(ModalClosingEventArgs e)
        {
            try
            {
                aggregating = true;
                cancelButtonProps.Disabled = true;
                StateHasChanged();
                var ids = selectedRows != null ? selectedRows.Select(c => c.Id).ToList() : new();
                bool result;
                if (ids.Any())
                {
                    result = await OvertimeAggregateService.AggregateAsync(new OvertimeAggregateSearch()
                    {
                        UsersIds = ids,
                        Month = overtimeAggregateFilterModel.Month.ToInt(),
                        Year = overtimeAggregateFilterModel.Year.ToInt(),
                    });
                }
                else
                {
                    result = await OvertimeAggregateService.AggregateAsync(new OvertimeAggregateSearch()
                    {
                        CodeOrName = overtimeAggregateFilterModel.CodeOrName,
                        Month = overtimeAggregateFilterModel.Month.ToInt(),
                        Year = overtimeAggregateFilterModel.Year.ToInt(),
                    });
                }
                if (result)
                {
                    Notice.NotiSuccess("Tổng hợp dữ liệu thành công");
                    overtimeAggregateFilterModel.Page.PageIndex = 1;
                    if (overtimeAggregateFilterModel.UsersId != null)
                    {
                        await LoadDataAsync();
                    }
                }
                else
                {
                    Notice.NotiError("Tổng hợp dữ liệu thất bại");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                aggregating = false;
                StateHasChanged();
            }
        }
    }
}
