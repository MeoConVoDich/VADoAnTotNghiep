using AntDesign;
using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using OneOf.Types;
using System.Collections.Generic;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.ViewModel;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using AntDesign.TableModels;
using System.Linq;
using DoAnTotNghiep.Config;
using System.Text.Json;

namespace DoAnTotNghiep.Systems
{
    public partial class PermissionGroupList : ComponentBase
    {
        [Inject] PermissionGroupService PermissionGroupService { get; set; }
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }

        List<PermissionGroupViewModel> PermissionGroupViewModels { get; set; }
        List<PermissionGroup> PermissionGroups { get; set; }
        List<string> selectedRowIds = new List<string>();
        PermissionGroupFilterEditModel permissionGroupEditFilterModel;
        IEnumerable<PermissionGroupViewModel> selectedRows;
        Table<PermissionGroupViewModel> table;
        PermissionGroupEditModel editModel = new PermissionGroupEditModel();
        SetClaim setClaimComponent;
        InputWatcher inputWatcher;
        bool detailVisible;
        bool loading;
        bool setClaimVisible;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                permissionGroupEditFilterModel = new PermissionGroupFilterEditModel();
                permissionGroupEditFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                if (PermissionClaim.ROLE_VIEW)
                {
                    await LoadDataAsync();

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
                selectedRows = null;
                StateHasChanged();
                var search = Mapper.Map<PermissionGroupSearch>(permissionGroupEditFilterModel);
                var data = await PermissionGroupService.GetPageWithFilterAsync(search);
                PermissionGroups = data.Item1 ?? new List<PermissionGroup>();
                permissionGroupEditFilterModel.Page.Total = data.Item2;
                PermissionGroupViewModels = Mapper.Map<List<PermissionGroupViewModel>>(PermissionGroups);
                int stt = permissionGroupEditFilterModel.Page.PageSize * (permissionGroupEditFilterModel.Page.PageIndex - 1) + 1;
                PermissionGroupViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
            loading = false;
        }

        void OnRowClick(RowData<PermissionGroupViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = PermissionGroups.FirstOrDefault(c => c.Id == rowData.Data.Id);
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

        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                if (e.Page > 0)
                {
                    permissionGroupEditFilterModel.Page.PageIndex = e.Page;
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
                permissionGroupEditFilterModel.Page.PageIndex = 1;
                permissionGroupEditFilterModel.Page.PageSize = e.PageSize;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchAsync(string search)
        {
            try
            {
                permissionGroupEditFilterModel.CodeOrName = search;
                permissionGroupEditFilterModel.Page.PageIndex = 1;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OpenDetail(PermissionGroupViewModel permissionGroupViewModel, bool edit)
        {
            try
            {
                var permissionGroup = PermissionGroups.FirstOrDefault(c => c.Id == permissionGroupViewModel.Id);
                editModel = Mapper.Map<PermissionGroupEditModel>(permissionGroup);
                editModel.ReadOnly = edit;
                detailVisible = true;
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
                editModel = new PermissionGroupEditModel();
                detailVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseDetail()
        {
            try
            {
                detailVisible = false;
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
                var existData = await PermissionGroupService.GetAllWithFilterAsync(new PermissionGroupSearch()
                {
                    Code = editModel.CodeGroup,
                    CheckExist = true
                });
                if (existData.Item1?.Any(c => c.Id != editModel.Id) == true)
                {
                    Notice.NotiWarning("Mã nhóm quyền đã tồn tại trong hệ thống");
                    return;
                }
                var PermissionGroup = Mapper.Map<PermissionGroup>(editModel);
                if (PermissionGroup.Id.IsNotNullOrEmpty())
                {
                    result = await PermissionGroupService.UpdateAsync(PermissionGroup);
                }
                else
                {
                    PermissionGroup.Id = ObjectExtentions.GenerateGuid();
                    PermissionGroup.CreateDate = DateTime.Now;
                    result = await PermissionGroupService.AddAsync(PermissionGroup);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    detailVisible = false;
                    await LoadDataAsync();
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

        void OpenSetClaim(PermissionGroupViewModel appRoleViewModel)
        {
            try
            {
                var permissions = JsonSerializer.Deserialize<List<string>>(appRoleViewModel.Permission ?? "[]");
                setClaimComponent.LoadClaim(permissions, false, appRoleViewModel.Id);
                setClaimVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(PermissionGroupViewModel model = null)
        {
            try
            {
                bool result = new();
                if (model != null)
                {
                    var deleteModel = PermissionGroups.FirstOrDefault(c => c.Id == model.Id);
                    result = await PermissionGroupService.DeleteAsync(deleteModel);
                }
                else
                {
                    if (selectedRows.Any() != true)
                    {
                        Notice.NotiError("Không có nhóm tài khoản được chọn!");
                        return;
                    }
                    var deleteList = PermissionGroups.Where(c => selectedRowIds.Contains(c.Id)).ToList();
                    result = await PermissionGroupService.DeleteListAsync(deleteList);
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

        void CloseSetClaim()
        {
            setClaimVisible = false;
        }

        async Task ClaimChangedAsync()
        {
            try
            {
                setClaimVisible = false;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit()
        {
            editModel.ReadOnly = false;
        }

        void CancelClick()
        {
            detailVisible = false;
        }
    }
}
