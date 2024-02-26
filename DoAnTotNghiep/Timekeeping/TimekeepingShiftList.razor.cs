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
    public partial class TimekeepingShiftList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] TimekeepingShiftService TimekeepingShiftService { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }

        List<TimekeepingShift> TimekeepingShifts = new List<TimekeepingShift>();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<TimekeepingShiftViewModel> TimekeepingShiftViewModels = new List<TimekeepingShiftViewModel>();
        Table<TimekeepingShiftViewModel> table;
        TimekeepingShiftFilterEditModel timekeepingShiftFilterModel = new TimekeepingShiftFilterEditModel();
        TimekeepingShiftEditModel editModel = new TimekeepingShiftEditModel();
        InputWatcher inputWatcher;
        TimekeepingShift selectModel = new TimekeepingShift();
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                timekeepingShiftFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                editModel.ReadOnly = true;
                await LoadDataAsync();
                await LoadTimekeepingTypeAsync();
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
                var search = Mapper.Map<TimekeepingShiftSearch>(timekeepingShiftFilterModel);
                var page = await TimekeepingShiftService.GetPageWithFilterAsync(search);
                TimekeepingShifts = page.Item1 ?? new List<TimekeepingShift>();
                TimekeepingShiftViewModels = Mapper.Map<List<TimekeepingShiftViewModel>>(TimekeepingShifts ?? new List<TimekeepingShift>());
                int stt = timekeepingShiftFilterModel.Page.PageSize * (timekeepingShiftFilterModel.Page.PageIndex - 1) + 1;
                TimekeepingShiftViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                timekeepingShiftFilterModel.Page.Total = page.Item2;
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
                var page = await TimekeepingTypeService.GetAllWithFilterAsync(new TimekeepingTypeSearch() 
                {
                    EffectiveState = EffectiveState.Active
                });
                TimekeepingTypes = page.Item1 ?? new List<TimekeepingType>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnRowClick(RowData<TimekeepingShiftViewModel> rowData)
        {
            try
            {
                selectModel = TimekeepingShifts.FirstOrDefault(c => c.Id == rowData.Data.Id);
                editModel = Mapper.Map<TimekeepingShiftEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(TimekeepingShiftEditModel model = null)
        {
            try
            {
                bool result = new();
                if (selectModel != null)
                {
                    result = await TimekeepingShiftService.DeleteAsync(selectModel);
                }
                if (result)
                {
                    Notice.NotiSuccess("Xoá dữ liệu thành công!");
                    editModel = new TimekeepingShiftEditModel();
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
                    timekeepingShiftFilterModel.Page.PageIndex = e.Page;
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
                timekeepingShiftFilterModel.Page.PageIndex = 1;
                timekeepingShiftFilterModel.Page.PageSize = e.PageSize;
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
                timekeepingShiftFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
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
                timekeepingShiftFilterModel.EffectiveState = effectiveState;
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
                editModel = new TimekeepingShiftEditModel();
                editModel.EffectiveState = EffectiveState.Active;
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
                var exist = (await TimekeepingShiftService.GetAllWithFilterAsync(new TimekeepingShiftSearch()
                {
                    CheckExist = true,
                    Code = editModel.Code
                })).Item1;

                if (exist.Any(c => c.Id != editModel.Id))
                {
                    Notice.NotiWarning("Mã ca đã được sử dụng");
                    return;
                }

                var timekeepingType = Mapper.Map<TimekeepingShift>(editModel);
                if (timekeepingType.Id.IsNotNullOrEmpty())
                {
                    result = await TimekeepingShiftService.UpdateAsync(timekeepingType);
                }
                else
                {
                    timekeepingType.Id = ObjectExtentions.GenerateGuid();
                    timekeepingType.CreateDate = DateTime.Now;
                    result = await TimekeepingShiftService.AddAsync(timekeepingType);
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
                editModel = Mapper.Map<TimekeepingShiftEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void BreaksTimeTypeChange(string breaksTimeType)
        {
            try
            {
                editModel.BreaksTimeType = Enum.Parse<BreaksTimeType>(breaksTimeType);
                editModel.TimeCalculator();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
