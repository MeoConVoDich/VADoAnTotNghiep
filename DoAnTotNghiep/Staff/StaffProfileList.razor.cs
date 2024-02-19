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
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Staff
{
    public partial class StaffProfileList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }

        List<Users> ListUsers = new List<Users>();
        List<UsersViewModel> UsersViewModels = new List<UsersViewModel>();
        IEnumerable<UsersViewModel> selectedRows;
        Table<UsersViewModel> table;
        UsersFilterEditModel usersFilterModel = new UsersFilterEditModel();
        List<string> selectedRowIds = new List<string>();
        StaffProfileDetail staffProfileDetail;
        bool loading;
        bool detailVisible;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                usersFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<UsersSearch>(usersFilterModel);
                var page = await UsersService.GetPageWithFilterAsync(search);
                ListUsers = page.Item1 ?? new List<Users>();
                UsersViewModels = Mapper.Map<List<UsersViewModel>>(ListUsers ?? new List<Users>());
                int stt = usersFilterModel.Page.PageSize * (usersFilterModel.Page.PageIndex - 1) + 1;
                UsersViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                usersFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<UsersViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = ListUsers.FirstOrDefault(c => c.Id == rowData.Data.Id);
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

        async Task AddNewAsync()
        {
            try
            {
                Users users = new Users();
                await staffProfileDetail.LoadEditModelAsync(users);
                detailVisible = true;
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
                    var deleteModel = ListUsers.FirstOrDefault(c => c.Id == model.Id);
                    result = await UsersService.DeleteAsync(deleteModel);
                }
                else
                {
                    if (selectedRows.Any() != true)
                    {
                        Notice.NotiError("Không có nhân viên được chọn!");
                        return;
                    }
                    var deleteList = ListUsers.Where(c => selectedRowIds.Contains(c.Id)).ToList();
                    result = await UsersService.DeleteListAsync(deleteList);
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


        async Task ViewDetailAsync(UsersViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = ListUsers.FirstOrDefault(c => c.Id == model.Id);
                await staffProfileDetail.LoadEditModelAsync(data, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task OpenDetailAsync(UsersViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = ListUsers.FirstOrDefault(c => c.Id == model.Id);
                await staffProfileDetail.LoadEditModelAsync(data);
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
                    usersFilterModel.Page.PageIndex = e.Page;
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
                usersFilterModel.Page.PageIndex = 1;
                usersFilterModel.Page.PageSize = e.PageSize;
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
                usersFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
