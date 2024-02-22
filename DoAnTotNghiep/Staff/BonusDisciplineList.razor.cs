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
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Staff
{
    public partial class BonusDisciplineList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] BonusDisciplineService BonusDisciplineService { get; set; }
        List<BonusDiscipline> BonusDisciplines = new List<BonusDiscipline>();
        List<BonusDisciplineViewModel> BonusDisciplineViewModels = new List<BonusDisciplineViewModel>();
        IEnumerable<BonusDisciplineViewModel> selectedRows;
        Table<BonusDisciplineViewModel> table;
        BonusDisciplineFilterEditModel bonusDisciplineFilterModel = new BonusDisciplineFilterEditModel();
        List<string> selectedRowIds = new List<string>();
        BonusDisciplineDetail bonusDisciplineDetail;
        bool loading;
        bool detailVisible;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                bonusDisciplineFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<BonusDisciplineSearch>(bonusDisciplineFilterModel);
                var page = await BonusDisciplineService.GetPageWithFilterAsync(search);
                BonusDisciplines = page.Item1 ?? new List<BonusDiscipline>();
                BonusDisciplineViewModels = Mapper.Map<List<BonusDisciplineViewModel>>(BonusDisciplines ?? new List<BonusDiscipline>());
                int stt = bonusDisciplineFilterModel.Page.PageSize * (bonusDisciplineFilterModel.Page.PageIndex - 1) + 1;
                BonusDisciplineViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                bonusDisciplineFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<BonusDisciplineViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = BonusDisciplines.FirstOrDefault(c => c.Id == rowData.Data.Id);
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

        void AddNew()
        {
            try
            {
                BonusDiscipline bonusDiscipline = new BonusDiscipline();
                bonusDiscipline.Date = DateTime.Now;
                bonusDiscipline.EffectiveState = EffectiveState.Active;
                bonusDisciplineDetail.LoadEditModel(bonusDiscipline);
                detailVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(BonusDisciplineViewModel model = null)
        {
            try
            {
                bool result = new();
                if (model != null)
                {
                    var deleteModel = BonusDisciplines.FirstOrDefault(c => c.Id == model.Id);
                    result = await BonusDisciplineService.DeleteAsync(deleteModel);
                }
                else
                {
                    if (selectedRows.Any() != true)
                    {
                        Notice.NotiError("Không có quyết định được chọn!");
                        return;
                    }
                    var deleteList = BonusDisciplines.Where(c => selectedRowIds.Contains(c.Id)).ToList();
                    result = await BonusDisciplineService.DeleteListAsync(deleteList);
                }
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

        void ViewDetail(BonusDisciplineViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = BonusDisciplines.FirstOrDefault(c => c.Id == model.Id);
                bonusDisciplineDetail.LoadEditModel(data, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OpenDetail(BonusDisciplineViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = BonusDisciplines.FirstOrDefault(c => c.Id == model.Id);
                bonusDisciplineDetail.LoadEditModel(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseDetail()
        {
            detailVisible = false;
        }

        async Task SaveDetailAsync()
        {
            try
            {
                detailVisible = false;
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
                    bonusDisciplineFilterModel.Page.PageIndex = e.Page;
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
                bonusDisciplineFilterModel.Page.PageIndex = 1;
                bonusDisciplineFilterModel.Page.PageSize = e.PageSize;
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
                bonusDisciplineFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task BonusDisciplineTypeChange(string bonusDisciplineType)
        {
            try
            {
                bonusDisciplineFilterModel.BonusDisciplineType = bonusDisciplineType;
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
                bonusDisciplineFilterModel.EffectiveState = effectiveState;
                await LoadDataAsync();
                StateHasChanged();
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
                bonusDisciplineFilterModel.FromDate = dateTimes[0];
                bonusDisciplineFilterModel.ToDate = dateTimes[1];
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
