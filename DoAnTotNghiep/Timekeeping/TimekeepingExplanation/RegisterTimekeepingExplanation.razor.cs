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

namespace DoAnTotNghiep.Timekeeping.TimekeepingExplanation
{
    public partial class RegisterTimekeepingExplanation
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] TimekeepingExplanationService TimekeepingExplanationService { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }

        List<Domain.TimekeepingExplanation> Vacations = new List<Domain.TimekeepingExplanation>();
        List<TimekeepingExplanationViewModel> TimekeepingExplanationViewModels = new List<TimekeepingExplanationViewModel>();
        Table<TimekeepingExplanationViewModel> table;
        TimekeepingExplanationFilterEditModel timekeepingExplanationFilterModel = new TimekeepingExplanationFilterEditModel();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        TimekeepingExplanationDetail timekeepingExplanationDetail;
        bool loading;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                if (usersId != null)
                {
                    timekeepingExplanationFilterModel.UsersId = usersId;
                    timekeepingExplanationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<TimekeepingExplanationSearch>(timekeepingExplanationFilterModel);
                var page = await TimekeepingExplanationService.GetPageWithFilterAsync(search);
                Vacations = page.Item1 ?? new List<Domain.TimekeepingExplanation>();
                TimekeepingExplanationViewModels = Mapper.Map<List<TimekeepingExplanationViewModel>>(Vacations ?? new List<Domain.TimekeepingExplanation>());
                int stt = timekeepingExplanationFilterModel.Page.PageSize * (timekeepingExplanationFilterModel.Page.PageIndex - 1) + 1;
                TimekeepingExplanationViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                    c.TimekeepingTypeName = TimekeepingTypes.FirstOrDefault(v => v.Id == c.TimekeepingTypeId)?.CodeName;
                });
                timekeepingExplanationFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<TimekeepingExplanationViewModel> rowData)
        {
            try
            {
                var select = Vacations.FirstOrDefault(c => c.Id == rowData.Data.Id);
                timekeepingExplanationDetail.LoadEditModel(select, true);
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
                    timekeepingExplanationFilterModel.Page.PageIndex = e.Page;
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
                timekeepingExplanationFilterModel.Page.PageIndex = 1;
                timekeepingExplanationFilterModel.Page.PageSize = e.PageSize;
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
                timekeepingExplanationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                timekeepingExplanationFilterModel.ApprovalStatus = approvalStatus;
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
                timekeepingExplanationFilterModel.Year = year;
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
                timekeepingExplanationFilterModel.Month = month;
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
