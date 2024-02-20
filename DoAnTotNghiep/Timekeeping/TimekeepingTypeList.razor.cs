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
    public partial class TimekeepingTypeList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }

        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<TimekeepingTypeViewModel> TimekeepingTypeViewModels = new List<TimekeepingTypeViewModel>();
        Table<TimekeepingTypeViewModel> table;
        TimekeepingTypeFilterEditModel timekeepingFilterModel = new TimekeepingTypeFilterEditModel();
        TimekeepingTypeEditModel editModel = new TimekeepingTypeEditModel();
        InputWatcher inputWatcher;
        TimekeepingType selectModel = new TimekeepingType();
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                timekeepingFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<TimekeepingTypeSearch>(timekeepingFilterModel);
                var page = await TimekeepingTypeService.GetPageWithFilterAsync(search);
                TimekeepingTypes = page.Item1 ?? new List<TimekeepingType>();
                TimekeepingTypeViewModels = Mapper.Map<List<TimekeepingTypeViewModel>>(TimekeepingTypes ?? new List<TimekeepingType>());
                int stt = timekeepingFilterModel.Page.PageSize * (timekeepingFilterModel.Page.PageIndex - 1) + 1;
                TimekeepingTypeViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                timekeepingFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<TimekeepingTypeViewModel> rowData)
        {
            try
            {
                selectModel = TimekeepingTypes.FirstOrDefault(c => c.Id == rowData.Data.Id);
                editModel = Mapper.Map<TimekeepingTypeEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(TimekeepingTypeEditModel model = null)
        {
            try
            {
                bool result = new();
                if (selectModel != null)
                {
                    result = await TimekeepingTypeService.DeleteAsync(selectModel);
                }
                if (result)
                {
                    Notice.NotiSuccess("Xoá dữ liệu thành công!");
                    editModel = new TimekeepingTypeEditModel();
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
                    timekeepingFilterModel.Page.PageIndex = e.Page;
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
                timekeepingFilterModel.Page.PageIndex = 1;
                timekeepingFilterModel.Page.PageSize = e.PageSize;
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
                timekeepingFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                timekeepingFilterModel.EffectiveState = effectiveState;
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
                editModel = new TimekeepingTypeEditModel();
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
                var exist = (await TimekeepingTypeService.GetAllWithFilterAsync(new TimekeepingTypeSearch()
                {
                    CheckExist = true,
                    Code = editModel.Code
                })).Item1;

                if (exist.Any(c => c.Id != editModel.Id))
                {
                    Notice.NotiWarning("Mã kiểu công đã được sử dụng");
                    return;
                }

                var timekeepingType = Mapper.Map<TimekeepingType>(editModel);
                if (timekeepingType.Id.IsNotNullOrEmpty())
                {
                    result = await TimekeepingTypeService.UpdateAsync(timekeepingType);
                }
                else
                {
                    timekeepingType.Id = ObjectExtentions.GenerateGuid();
                    timekeepingType.CreateDate = DateTime.Now;
                    result = await TimekeepingTypeService.AddAsync(timekeepingType);
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
                editModel = Mapper.Map<TimekeepingTypeEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
