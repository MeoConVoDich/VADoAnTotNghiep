using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.ViewModel;
using AntDesign;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using AntDesign.TableModels;
using DoAnTotNghiep.Config;
using Blazored.SessionStorage;

namespace DoAnTotNghiep.Timekeeping.Overtime
{
    public partial class RegisterOvertime : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] OvertimeService OvertimeService { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }

        List<Domain.Overtime> Vacations = new List<Domain.Overtime>();
        List<OvertimeViewModel> OvertimeViewModels = new List<OvertimeViewModel>();
        Table<OvertimeViewModel> table;
        OvertimeFilterEditModel overtimeFilterModel = new OvertimeFilterEditModel();
        OvertimeDetail overtimeDetail;
        bool loading;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                if (usersId != null)
                {
                    overtimeFilterModel.UsersId = usersId;
                    overtimeFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                    await LoadDataAsync();
                }
                else
                {
                    Notice.NotiWarning("Không tìm thấy tài khoản, yêu càu đăng nhập lại!");
                    return;
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
                StateHasChanged();
                var search = Mapper.Map<OvertimeSearch>(overtimeFilterModel);
                var page = await OvertimeService.GetPageWithFilterAsync(search);
                Vacations = page.Item1 ?? new List<Domain.Overtime>();
                OvertimeViewModels = Mapper.Map<List<OvertimeViewModel>>(Vacations ?? new List<Domain.Overtime>());
                int stt = overtimeFilterModel.Page.PageSize * (overtimeFilterModel.Page.PageIndex - 1) + 1;
                OvertimeViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                overtimeFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<OvertimeViewModel> rowData)
        {
            try
            {
                var select = Vacations.FirstOrDefault(c => c.Id == rowData.Data.Id);
                overtimeDetail.LoadEditModel(select, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SaveDetailAsync()
        {
            try
            {
                await SearchAsync();
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
                    overtimeFilterModel.Page.PageIndex = e.Page;
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
                overtimeFilterModel.Page.PageIndex = 1;
                overtimeFilterModel.Page.PageSize = e.PageSize;
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
                overtimeFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task ApprovalStatusChangeAsync(string approvalStatus)
        {
            try
            {
                overtimeFilterModel.ApprovalStatus = approvalStatus;
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
                overtimeFilterModel.Year = year;
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
                overtimeFilterModel.Month = month;
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
