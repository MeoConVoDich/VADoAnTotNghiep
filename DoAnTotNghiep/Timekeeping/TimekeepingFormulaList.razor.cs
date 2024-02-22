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
    public partial class TimekeepingFormulaList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] TimekeepingFormulaService TimekeepingFormulaService { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }

        List<TimekeepingFormula> TimekeepingFormulas = new List<TimekeepingFormula>();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<TimekeepingFormulaViewModel> TimekeepingFormulaViewModels = new List<TimekeepingFormulaViewModel>();
        Table<TimekeepingFormulaViewModel> table;
        TimekeepingFormulaFilterEditModel timekeepingFormulaFilterModel = new TimekeepingFormulaFilterEditModel();
        TimekeepingFormulaEditModel editModel = new TimekeepingFormulaEditModel();
        InputWatcher inputWatcher;
        TimekeepingFormula selectModel = new TimekeepingFormula();
        string search;
        bool loading;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                timekeepingFormulaFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<TimekeepingFormulaSearch>(timekeepingFormulaFilterModel);
                var page = await TimekeepingFormulaService.GetPageWithFilterAsync(search);
                TimekeepingFormulas = page.Item1 ?? new List<TimekeepingFormula>();
                TimekeepingFormulaViewModels = Mapper.Map<List<TimekeepingFormulaViewModel>>(TimekeepingFormulas ?? new List<TimekeepingFormula>());
                int stt = timekeepingFormulaFilterModel.Page.PageSize * (timekeepingFormulaFilterModel.Page.PageIndex - 1) + 1;
                TimekeepingFormulaViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                timekeepingFormulaFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<TimekeepingFormulaViewModel> rowData)
        {
            try
            {
                selectModel = TimekeepingFormulas.FirstOrDefault(c => c.Id == rowData.Data.Id);
                editModel = Mapper.Map<TimekeepingFormulaEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(TimekeepingFormulaEditModel model = null)
        {
            try
            {
                bool result = new();
                if (selectModel != null)
                {
                    result = await TimekeepingFormulaService.DeleteAsync(selectModel);
                }
                if (result)
                {
                    Notice.NotiSuccess("Xoá dữ liệu thành công!");
                    editModel = new TimekeepingFormulaEditModel();
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
                    timekeepingFormulaFilterModel.Page.PageIndex = e.Page;
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
                timekeepingFormulaFilterModel.Page.PageIndex = 1;
                timekeepingFormulaFilterModel.Page.PageSize = e.PageSize;
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
                timekeepingFormulaFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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

        async Task AddAsync()
        {
            try
            {
                var countCode = await TimekeepingFormulaService.GetCountCodeAsync();
                editModel = new TimekeepingFormulaEditModel();
                editModel.CountCode = countCode;
                editModel.Code = string.Format("{0}{1:000}", "CTCC", countCode);
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
                var timekeepingType = Mapper.Map<TimekeepingFormula>(editModel);
                if (timekeepingType.Id.IsNotNullOrEmpty())
                {
                    result = await TimekeepingFormulaService.UpdateAsync(timekeepingType);
                }
                else
                {
                    timekeepingType.Id = ObjectExtentions.GenerateGuid();
                    timekeepingType.CreateDate = DateTime.Now;
                    result = await TimekeepingFormulaService.AddAsync(timekeepingType);
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
                editModel = Mapper.Map<TimekeepingFormulaEditModel>(selectModel);
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void InsertTimeKeepingTypeElementAsync(string item)
        {
            try
            {
                if (!editModel.ReadOnly)
                {
                    editModel.Formula = $"{editModel.Formula}[{item}]";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
