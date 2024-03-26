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
    public partial class TimekeepingAggregateList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] TimekeepingAggregateService TimekeepingAggregateService { get; set; }
        [Inject] SummaryOfTimekeepingService SummaryOfTimekeepingService { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }
        [Inject] TimekeepingFormulaService TimekeepingFormulaService { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Inject] ModalService ModalService { get; set; }
        TimekeepingAggregateFilterEditModel timekeepingAggregateFilterModel = new TimekeepingAggregateFilterEditModel();
        UsersSearch usersSearch = new UsersSearch();
        List<Users> ListUsers = new List<Users>();
        List<UsersViewModel> UsersViewModels = new List<UsersViewModel>();
        List<TimekeepingAggregate> TimekeepingAggregates = new List<TimekeepingAggregate>();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<TimekeepingFormula> TimekeepingFormulas = new List<TimekeepingFormula>();
        List<TimekeepingAggregateViewModel> TimekeepingAggregateViewModels = new List<TimekeepingAggregateViewModel>();
        List<TimekeepingAggregate> TimekeepingAggregateExcelModels = new List<TimekeepingAggregate>();
        List<TimekeepingSummaryViewModel> TimekeepingSummaryViewModels = new List<TimekeepingSummaryViewModel>();
        TimekeepingAggregateViewModel summary = new TimekeepingAggregateViewModel();
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
                timekeepingAggregateFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                if (PermissionClaim.TIMEKEEPINGAGGREGATE_VIEW)
                {
                    await LoadUsersAsync();
                    await LoadTimekeepingTypeAsync();
                    await LoadTimekeepingFormulaAsync();
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

        async Task LoadTimekeepingTypeAsync()
        {
            try
            {
                var data = await TimekeepingTypeService.GetAllWithFilterAsync(new TimekeepingTypeSearch()
                {
                    EffectiveState = EffectiveState.Active,
                });
                TimekeepingTypes = data.Item1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadTimekeepingFormulaAsync()
        {
            try
            {
                var data = await TimekeepingFormulaService.GetAllWithFilterAsync(new TimekeepingFormulaSearch());
                TimekeepingFormulas = data.Item1;
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
                StateHasChanged();
                summary = new TimekeepingAggregateViewModel();
                var search = Mapper.Map<TimekeepingAggregateSearch>(timekeepingAggregateFilterModel);
                var page = await TimekeepingAggregateService.GetAllWithFilterAsync(search);
                TimekeepingAggregates = page.Item1;
                timekeepingAggregateFilterModel.Page.Total = page.Item2;
                GetPageData();
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
                var data = TimekeepingAggregates.Skip(timekeepingAggregateFilterModel.Page.PageSize * (timekeepingAggregateFilterModel.Page.PageIndex - 1))
                    .Take(timekeepingAggregateFilterModel.Page.PageSize).ToList();
                TimekeepingAggregateViewModels = Mapper.Map<List<TimekeepingAggregateViewModel>>(data ?? new List<TimekeepingAggregate>());
                int stt = timekeepingAggregateFilterModel.Page.PageSize * (timekeepingAggregateFilterModel.Page.PageIndex - 1) + 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadSummaryOfTimekeepingAsync()
        {
            try
            {
                TimekeepingSummaryViewModels = new List<TimekeepingSummaryViewModel>();
                var search = Mapper.Map<SummaryOfTimekeepingSearch>(timekeepingAggregateFilterModel);
                var data = await SummaryOfTimekeepingService.GetWithFilterAsync(search);
                var summaryOfTimekeepingView = Mapper.Map<SummaryOfTimekeepingViewModel>(data);
                var dataTypes = summaryOfTimekeepingView.DataType.Where(c => c.Value != "0");
                TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(nameof(summaryOfTimekeepingView.StandradDay),"Công chuẩn",
                                                               $"{summaryOfTimekeepingView.StandradDay} công"));
                TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(nameof(summaryOfTimekeepingView.WorkLateMinutes), "Số phút đi muộn",
                                                                $"{summaryOfTimekeepingView.WorkLateMinutes} phút"));
                TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(nameof(summaryOfTimekeepingView.LeaveEarlyMinutes), "Số phút về sớm",
                                                               $"{summaryOfTimekeepingView.LeaveEarlyMinutes} phút"));
                TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(nameof(summaryOfTimekeepingView.OvertimeHour), "Số giờ làm thêm",
                                                               $"{summaryOfTimekeepingView.OvertimeHour.ToDecimalUnFormatDot()} giờ"));
                foreach (var dataType in dataTypes)
                {
                    TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(dataType.Key, GetTimekeepingTypeName(dataType.Key), dataType.Value.ToDecimalUnFormat()));
                }
                foreach (var dataFormula in summaryOfTimekeepingView.DataFormula)
                {
                    TimekeepingSummaryViewModels.Add(new TimekeepingSummaryViewModel(dataFormula.Key, GetFormulaName(dataFormula.Key), dataFormula.Value.ToDecimalUnFormat()));
                }

                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        string GetTimekeepingTypeName(string key)
        {
            try
            {
                var type = TimekeepingTypes?.FirstOrDefault(c => c.Code == key);
                if (type != null)
                {
                    return $"{type.Code} - {type.Name}";
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        string GetFormulaName(string key)
        {
            try
            {
                var formula = TimekeepingFormulas?.FirstOrDefault(c => c.Code == key);
                if (formula != null)
                {
                    return $"{formula.Name}";
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
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
                if (timekeepingAggregateFilterModel.UsersId.IsNotNullOrEmpty())
                {
                    await LoadDataAsync();
                    await LoadSummaryOfTimekeepingAsync();
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
                timekeepingAggregateFilterModel.Year = year;
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
                timekeepingAggregateFilterModel.Month = month;
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
                timekeepingAggregateFilterModel.Page.PageIndex = e.Page;
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
                timekeepingAggregateFilterModel.Page.PageIndex = 1;
                timekeepingAggregateFilterModel.Page.PageSize = e.PageSize;
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
                if (timekeepingAggregateFilterModel.Year.IsNotNullOrEmpty() && timekeepingAggregateFilterModel.Month.IsNotNullOrEmpty() && rowData.IsNotNullOrEmpty())
                {
                    timekeepingAggregateFilterModel.UsersId = rowData.Data.Id;
                    await LoadDataAsync();
                    await LoadSummaryOfTimekeepingAsync();
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
                    result = await TimekeepingAggregateService.AggregateAsync(new TimekeepingAggregateSearch()
                    {
                        UsersIds = ids,
                        Month = timekeepingAggregateFilterModel.Month.ToInt(),
                        Year = timekeepingAggregateFilterModel.Year.ToInt(),
                    });
                }
                else
                {
                    result = await TimekeepingAggregateService.AggregateAsync(new TimekeepingAggregateSearch()
                    {
                        CodeOrName = timekeepingAggregateFilterModel.CodeOrName,
                        Month = timekeepingAggregateFilterModel.Month.ToInt(),
                        Year = timekeepingAggregateFilterModel.Year.ToInt(),
                    });
                }
                if (result)
                {
                    Notice.NotiSuccess("Tổng hợp dữ liệu thành công");
                    timekeepingAggregateFilterModel.Page.PageIndex = 1;
                    if (timekeepingAggregateFilterModel.UsersId != null)
                    {
                        await LoadDataAsync();
                        await LoadSummaryOfTimekeepingAsync();
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
