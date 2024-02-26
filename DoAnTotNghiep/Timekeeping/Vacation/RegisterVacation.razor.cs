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

namespace DoAnTotNghiep.Timekeeping.Vacation
{
    public partial class RegisterVacation : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] VacationService VacationService { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }

        List<Domain.Vacation> Vacations = new List<Domain.Vacation>();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<VacationViewModel> VacationViewModels = new List<VacationViewModel>();
        Table<VacationViewModel> table;
        VacationFilterEditModel vacationFilterModel = new VacationFilterEditModel();
        VacationDetail vacationDetail;
        bool loading;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                if (usersId != null)
                {
                    vacationFilterModel.UsersId = usersId;
                    vacationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                    await LoadTimekeepingTypeAsync();
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
                var search = Mapper.Map<VacationSearch>(vacationFilterModel);
                var page = await VacationService.GetPageWithFilterAsync(search);
                Vacations = page.Item1 ?? new List<Domain.Vacation>();
                VacationViewModels = Mapper.Map<List<VacationViewModel>>(Vacations ?? new List<Domain.Vacation>());
                int stt = vacationFilterModel.Page.PageSize * (vacationFilterModel.Page.PageIndex - 1) + 1;
                VacationViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                    c.TimekeepingTypeName = TimekeepingTypes.FirstOrDefault(v => v.Id == c.TimekeepingTypeId)?.CodeName;
                });
                vacationFilterModel.Page.Total = page.Item2;
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

        async Task LoadTimekeepingTypeAsync()
        {
            try
            {
                var page = await TimekeepingTypeService.GetAllWithFilterAsync(new TimekeepingTypeSearch() { EffectiveState = EffectiveState.Active });
                TimekeepingTypes = page.Item1 ?? new List<TimekeepingType>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnRowClick(RowData<VacationViewModel> rowData)
        {
            try
            {
                var select = Vacations.FirstOrDefault(c => c.Id == rowData.Data.Id);
                vacationDetail.LoadEditModel(select, true);
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
                    vacationFilterModel.Page.PageIndex = e.Page;
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
                vacationFilterModel.Page.PageIndex = 1;
                vacationFilterModel.Page.PageSize = e.PageSize;
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
                vacationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
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
                vacationFilterModel.ApprovalStatus = approvalStatus;
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
                vacationFilterModel.Year = year;
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
                vacationFilterModel.Month = month;
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
