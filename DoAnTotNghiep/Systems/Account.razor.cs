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
using Blazored.SessionStorage;

namespace DoAnTotNghiep.Systems
{
    public partial class Account : ComponentBase
    {
        [Inject] UsersService UsersService { get; set; }
        [Inject] PermissionGroupService PermissionGroupService { get; set; }
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }

        List<UsersViewModel> UsersViewModels { get; set; }
        List<Users> Userss { get; set; }
        List<PermissionGroup> PermissionGroups { get; set; } = new List<PermissionGroup>();
        List<string> selectedRowIds = new List<string>();
        List<Users> StaffDatas = new List<Users>();
        List<Users> StaffSearchDatas = new List<Users>();
        UsersFilterEditModel usersEditFilterModel;
        IEnumerable<UsersViewModel> selectedRows;
        Table<UsersViewModel> table;
        CreateUsers editModel = new CreateUsers();
        ChangePass editModelChangePass = new ChangePass();
        SetClaim setClaimComponent;
        InputWatcher inputWatcher;
        List<string> permissionGroupIds = new List<string>();
        Users select = new Users();
        bool detailVisible;
        bool changedPassVisible;
        bool loading;
        bool setClaimVisible;
        bool isAdmin;
        bool setPermissionGroupVisible;
        string usersId;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                usersEditFilterModel = new UsersFilterEditModel();
                usersEditFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                await LoadDataAsync();
                await LoadStaffDataAsync();
                await LoadPermissionGroupAsync();
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
                var search = Mapper.Map<UsersSearch>(usersEditFilterModel);
                search.GetOnlyNoUserName = true;
                var data = await UsersService.GetPageWithFilterAsync(search);
                Userss = data.Item1 ?? new List<Users>();
                usersEditFilterModel.Page.Total = data.Item2;
                UsersViewModels = Mapper.Map<List<UsersViewModel>>(Userss);
                int stt = usersEditFilterModel.Page.PageSize * (usersEditFilterModel.Page.PageIndex - 1) + 1;
                UsersViewModels.ForEach(c =>
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

        async Task LoadPermissionGroupAsync()
        {
            try
            {
                loading = true;
                selectedRows = null;
                StateHasChanged();
                var data = await PermissionGroupService.GetAllWithFilterAsync(new PermissionGroupSearch());
                PermissionGroups = data.Item1 ?? new List<PermissionGroup>();
            }
            catch (Exception ex)
            {
                throw;
            }
            loading = false;
        }

        void OnRowClick(RowData<UsersViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = Userss.FirstOrDefault(c => c.Id == rowData.Data.Id);
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
                    usersEditFilterModel.Page.PageIndex = e.Page;
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
                usersEditFilterModel.Page.PageIndex = 1;
                usersEditFilterModel.Page.PageSize = e.PageSize;
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
                usersEditFilterModel.Page.PageIndex = 1;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OpenDetail(UsersViewModel usersViewModel)
        {
            try
            {
                var users = Userss.FirstOrDefault(c => c.Id == usersViewModel.Id);
                editModel = Mapper.Map<CreateUsers>(users);
                editModel.ReadOnly = true;
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
                editModel = new CreateUsers();
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
                changedPassVisible = false;
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

                var existData = await UsersService.GetAllWithFilterAsync(new UsersSearch()
                {
                    UserName = editModel.UserName,
                    CheckExist = true
                });

                if (existData.Item1?.Any(c => c.Id != editModel.Id) == true)
                {
                    Notice.NotiWarning("Tên đăng nhập đã được sử dụng!");
                    return;
                }

                var data = await UsersService.GetAllWithFilterAsync(new UsersSearch()
                {
                    Id = editModel.Id,
                });

                var users = data.Item1.FirstOrDefault();
                if (users != null)
                {
                    users.UserName = editModel.UserName;
                    users.Password = editModel.Password;
                }
                else
                {
                    Notice.NotiWarning("Không tìm thấy nhân viên!");
                    return;
                }
                var result = await UsersService.UpdateAsync(users);
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

        async Task SaveChangePassAsync()
        {
            try
            {
                bool result = new();
                var errorMessageStore = editModelChangePass.ValidateAll();
                if (!inputWatcher.Validate() || errorMessageStore.Any())
                {
                    if (errorMessageStore.Any())
                    {
                        inputWatcher.NotifyFieldChanged(errorMessageStore.First().Key, errorMessageStore);
                    }
                    Notice.NotiWarning("Dữ liệu còn thiếu hoặc không hợp lệ!");
                    return;
                }

                var data = await UsersService.GetAllWithFilterAsync(new UsersSearch()
                {
                    Id = editModelChangePass.Id,
                });

                var users = data.Item1.FirstOrDefault();
                if (users != null)
                {
                    users.Password = editModelChangePass.ChangePassword;
                }
                else
                {
                    Notice.NotiWarning("Không tìm thấy nhân viên!");
                    return;
                }

                if (users.Id.IsNotNullOrEmpty())
                {
                    result = await UsersService.UpdateAsync(users);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    changedPassVisible = false;
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

        void OpenSetClaim(UsersViewModel appRoleViewModel)
        {
            try
            {
                var permissions = JsonSerializer.Deserialize<List<string>>(appRoleViewModel.Permission ?? "[]");
                setClaimComponent.LoadClaim(permissions, appRoleViewModel.IsAdmin, appRoleViewModel.Id);
                setClaimVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(UsersViewModel model = null)
        {
            try
            {

                bool result = new();
                if (model != null)
                {
                    if (model.Id == usersId)
                    {
                        Notice.NotiError("Không thể tài khoản của chính bạn!");
                        return;
                    }
                    var deleteModel = Userss.FirstOrDefault(c => c.Id == model.Id);
                    deleteModel.Password = null;
                    deleteModel.UserName = null;
                    deleteModel.IsAdmin = false;
                    deleteModel.PermissionGroup = null;
                    deleteModel.Permission = null;
                    result = await UsersService.UpdateAsync(deleteModel);
                }
                else
                {
                    if (selectedRows.Any() != true)
                    {
                        Notice.NotiError("Không có nhóm tài khoản được chọn!");
                        return;
                    }
                    if (selectedRowIds.Contains(usersId))
                    {
                        Notice.NotiError("Không thể tài khoản của chính bạn!");
                        return;
                    }
                    var deleteList = Userss.Where(c => selectedRowIds.Contains(c.Id)).ToList();
                    deleteList.ForEach(c =>
                    {
                        c.Password = null;
                        c.UserName = null;
                        c.IsAdmin = false;
                        c.PermissionGroup = null;
                        c.Permission = null;
                    });
                    result = await UsersService.UpdateListAsync(deleteList);
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

        void CancelClick()
        {
            detailVisible = false;
        }

        void StaffChange(string staffId)
        {
            try
            {
                var selectedStaff = StaffDatas.FirstOrDefault(c => c.Id == staffId);
                if (selectedStaff != null)
                {
                    editModel.Code = selectedStaff.Code;
                    editModel.Name = selectedStaff.Name;
                    editModel.Id = selectedStaff.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnStaffSearch(string e)
        {
            try
            {
                StaffSearchDatas.Clear();
                StaffSearchDatas = StaffDatas.Where(c => c.Name.Contains(e, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c.Name).Take(10).ToList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadStaffDataAsync()
        {
            try
            {
                var dataStaff = await UsersService.GetAllWithFilterAsync(new UsersSearch());
                StaffDatas = dataStaff.Item1 ?? new();
                StaffSearchDatas = StaffDatas.Take(5).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OpenChangePasswordForm(UsersViewModel usersViewModel)
        {
            try
            {
                var users = Userss.FirstOrDefault(c => c.Id == usersViewModel.Id);
                editModel = Mapper.Map<CreateUsers>(users);
                changedPassVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OpenSetPermissionGroupAsync(UsersViewModel usersViewModel)
        {
            try
            {
                select = Userss.FirstOrDefault(c => c.Id == usersViewModel.Id);
                permissionGroupIds = JsonSerializer.Deserialize<List<string>>(select.PermissionGroup ?? "[]");
                setPermissionGroupVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CheckChanged(bool value, PermissionGroup permissionGroup)
        {
            try
            {
                if (isAdmin)
                {
                    return;
                }
                if (value)
                {
                    permissionGroupIds.Add(permissionGroup.Id);
                }
                else
                {
                    permissionGroupIds.Remove(permissionGroup.Id);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseSetPermissionGroup()
        {
            setPermissionGroupVisible = false;
        }

        async Task SavePermissionGroupAsync()
        {
            try
            {
                if(select.Id == usersId)
                {
                    Notice.NotiError("Không thế gán nhóm quyền cho chính bạn!");
                    return;
                }

                select.PermissionGroup = JsonSerializer.Serialize(permissionGroupIds);
                var result = await UsersService.UpdateAsync(select);
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    setPermissionGroupVisible = false;
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
    }
}
