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
    public partial class OvertimeRateList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] OvertimeRateService OvertimeRateService { get; set; }
        List<OvertimeRate> OvertimeRates = new List<OvertimeRate>();
        List<OvertimeRateViewModel> OvertimeRateViewModels = new List<OvertimeRateViewModel>();
        Table<OvertimeRateViewModel> table;
        OvertimeRateFilterEditModel overtimeRateFilterModel = new OvertimeRateFilterEditModel();
        OvertimeRateEditModel editModel = new OvertimeRateEditModel();
        InputWatcher inputWatcher;
        OvertimeRate selectModel = new OvertimeRate();
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                overtimeRateFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
                editModel.ReadOnly = true;
                await LoadDataAsync();
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
                var search = Mapper.Map<OvertimeRateSearch>(overtimeRateFilterModel);
                var page = await OvertimeRateService.GetPageWithFilterAsync(search);
                OvertimeRates = page.Item1 ?? new List<OvertimeRate>();
                OvertimeRateViewModels = Mapper.Map<List<OvertimeRateViewModel>>(OvertimeRates ?? new List<OvertimeRate>());
                int stt = overtimeRateFilterModel.Page.PageSize * (overtimeRateFilterModel.Page.PageIndex - 1) + 1;
                OvertimeRateViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                overtimeRateFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<OvertimeRateViewModel> rowData)
        {
            try
            {
                selectModel = OvertimeRates.FirstOrDefault(c => c.Id == rowData.Data.Id);
                editModel = Mapper.Map<OvertimeRateEditModel>(selectModel);
                editModel.ReadOnly = true;
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
                bool result = false;
                if (selectModel != null)
                {
                    result = await OvertimeRateService.DeleteAsync(selectModel);
                }
                if (result)
                {
                    Notice.NotiSuccess("Xoá dữ liệu thành công!");
                    selectModel = null;
                    editModel = new OvertimeRateEditModel();
                    editModel.ReadOnly = true;
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

        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                if (e.Page > 0)
                {
                    overtimeRateFilterModel.Page.PageIndex = e.Page;
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
                overtimeRateFilterModel.Page.PageIndex = 1;
                overtimeRateFilterModel.Page.PageSize = e.PageSize;
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
                overtimeRateFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task EffectiveStateChangeAsync(string effectiveState)
        {
            try
            {
                overtimeRateFilterModel.EffectiveState = effectiveState;
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
                editModel.ReadOnly = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Add()
        {
            try
            {
                editModel = new OvertimeRateEditModel();
                editModel.EffectiveState = EffectiveState.Active;
                editModel.Date = DateTime.Now;
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
                bool result = new();
                var errorMessageStore = editModel.ValidateAll();
                if (!inputWatcher.Validate() || errorMessageStore.Any())
                {
                    if (errorMessageStore.Any())
                    {
                        inputWatcher.NotifyFieldChanged(errorMessageStore.First().Key, errorMessageStore);
                    }
                    Notice.NotiWarning("Dữ liệu còn thiếu hoặc không hợp lệ!");
                    return;
                }
                var timekeepingType = Mapper.Map<OvertimeRate>(editModel);
                if (timekeepingType.Id.IsNotNullOrEmpty())
                {
                    result = await OvertimeRateService.UpdateAsync(timekeepingType);
                }
                else
                {
                    timekeepingType.Id = ObjectExtentions.GenerateGuid();
                    timekeepingType.CreateDate = DateTime.Now;
                    result = await OvertimeRateService.AddAsync(timekeepingType);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    editModel.ReadOnly = true;
                    await SearchAsync();
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

        void Cancel()
        {
            try
            {
                editModel = Mapper.Map<OvertimeRateEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SetViewDateAsync(DateTime?[] dateTimes)
        {
            try
            {
                overtimeRateFilterModel.FromDate = dateTimes[0];
                overtimeRateFilterModel.ToDate = dateTimes[1];
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
