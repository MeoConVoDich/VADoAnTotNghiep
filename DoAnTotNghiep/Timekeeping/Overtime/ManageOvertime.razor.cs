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

namespace DoAnTotNghiep.Timekeeping.Overtime
{
    public partial class ManageOvertime : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] OvertimeService OvertimeService { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        List<Domain.Overtime> Vacations = new List<Domain.Overtime>();
        List<OvertimeViewModel> selectedApproveRows = new();
        List<OvertimeViewModel> OvertimeViewModels = new List<OvertimeViewModel>();
        Table<OvertimeViewModel> table;
        OvertimeFilterEditModel overtimeFilterModel = new OvertimeFilterEditModel();
        OvertimeDetail overtimeDetail;
        IEnumerable<OvertimeViewModel> selectedRows;
        List<string> selectedRowIds = new List<string>();
        bool loading;
        bool detailVisible;
        bool cancelApproveModalVisible;
        bool cancelApproving;
        string cancelApproveReason;
        bool approvedModalVisible;
        bool approving;
        bool disapprovedModalVisible;
        bool disapproving;
        string disapprovedReason;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                overtimeFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                var search = Mapper.Map<OvertimeSearch>(overtimeFilterModel);
                var page = await OvertimeService.GetPageWithFilterAsync(search);
                Vacations = page.Item1 ?? new List<Domain.Overtime>();
                OvertimeViewModels = Mapper.Map<List<OvertimeViewModel>>(Vacations ?? new List<Domain.Overtime>());
                int stt = overtimeFilterModel.Page.PageSize * (overtimeFilterModel.Page.PageIndex - 1) + 1;
                OvertimeViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                overtimeFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<OvertimeViewModel> rowData)
        {
            try
            {
                List<string> ids;
                var selectData = Vacations.FirstOrDefault(c => c.Id == rowData.Data.Id);
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

        void CloseDetail()
        {
            detailVisible = false;
        }

        async Task PageIndexChangeAsync(PaginationEventArgs e)
        {
            try
            {
                if (e.Page > 0)
                {
                    overtimeFilterModel.Page.PageIndex = e.Page;
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
                overtimeFilterModel.Page.PageIndex = 1;
                overtimeFilterModel.Page.PageSize = e.PageSize;
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
                overtimeFilterModel.Page = new Page() { PageIndex = 1, PageSize = 10 };
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
                overtimeFilterModel.ApprovalStatus = approvalStatus;
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
                overtimeFilterModel.Year = year;
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
                overtimeFilterModel.Month = month;
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void ViewDetail(OvertimeViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                overtimeDetail.LoadEditModel(data, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit(OvertimeViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                overtimeDetail.LoadEditModel(data, data.CreatorObject != CreatorObject.HRStaff);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(OvertimeViewModel model = null)
        {
            try
            {
                bool result = new();
                if (model != null)
                {
                    if (model.CreatorObject == CreatorObject.HRStaff)
                    {
                        var deleteModel = Vacations.FirstOrDefault(c => c.Id == model.Id);
                        result = await OvertimeService.DeleteAsync(deleteModel);
                    }
                    else
                    {
                        Notice.NotiError("Không được xoá bản ghi do nhân viên tạo!");
                        return;
                    }
                }
                else
                {
                    if (selectedRows.Any() != true)
                    {
                        Notice.NotiError("Không có bản ghi được chọn!");
                        return;
                    }
                    if (selectedRows.Any(c => c.CreatorObject != CreatorObject.HRStaff))
                    {
                        Notice.NotiError("Không được xoá bản ghi do nhân viên tạo!");
                        return;
                    }
                    var deleteList = Vacations.Where(c => selectedRowIds.Contains(c.Id)).ToList();
                    result = await OvertimeService.DeleteListAsync(deleteList);
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

        void AddNew()
        {
            try
            {
                Domain.Overtime vacation = new Domain.Overtime();
                vacation.ApprovalStatus = ApprovalStatus.Approved;
                vacation.CreatorObject = CreatorObject.HRStaff;
                overtimeDetail.LoadEditModel(vacation);
                detailVisible = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        bool IsDisableApprovalButton()
        {
            return selectedRows?.Any() != true;
        }

        void ApprovedCheck(Domain.Overtime selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(OvertimeViewModels.FirstOrDefault(c => c.Id == selected.Id));
                }
                else if (selectedRows?.Any() == true)
                {
                    selectedApproveRows.AddRange(selectedRows);
                }
                if (selectedApproveRows.Any()
                    && selectedApproveRows.Any(c => c.ApprovalStatus != ApprovalStatus.Pending))
                {
                    Notice.NotiWarning("Chỉ được phê duyệt bản ghi đang chờ phê duyệt!");
                    return;
                }
                else if (!selectedApproveRows.Any())
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
                approvedModalVisible = true;
                approving = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        void DeclinedCheck(Domain.Overtime selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(OvertimeViewModels.FirstOrDefault(c => c.Id == selected.Id));
                }
                else if (selectedRows?.Any() == true)
                {
                    selectedApproveRows.AddRange(selectedRows);
                }
                if (selectedApproveRows.Any()
                    && selectedApproveRows.Any(c => c.ApprovalStatus != ApprovalStatus.Pending))
                {
                    Notice.NotiWarning("Chỉ được không phê duyệt bản ghi đang chờ phê duyệt!");
                    return;
                }
                else if (!selectedApproveRows.Any())
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
                disapprovedReason = null;
                disapprovedModalVisible = true;
                disapproving = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CancelApproveCheck(Domain.Overtime selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(OvertimeViewModels.FirstOrDefault(c => c.Id == selected.Id));
                }
                else if (selectedRows?.Any() == true)
                {
                    selectedApproveRows.AddRange(selectedRows);
                }
                if (selectedApproveRows.Any()
                    && selectedApproveRows.Any(c => c.ApprovalStatus != ApprovalStatus.Approved))
                {
                    Notice.NotiWarning("Chỉ được huỷ phê duyệt bản ghi đang chờ phê duyệt!");
                    return;
                }
                else if (!selectedApproveRows.Any())
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
                cancelApproveReason = null;
                cancelApproveModalVisible = true;
                cancelApproving = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseApprovedModal()
        {
            try
            {
                approvedModalVisible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseDisapprovedModal()
        {
            try
            {
                disapprovedModalVisible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void CloseCancelApproveModal()
        {
            try
            {
                cancelApproveModalVisible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task ApprovedAsync()
        {
            try
            {
                if (selectedApproveRows.Any())
                {
                    approving = true;
                    StateHasChanged();
                    var listId = selectedApproveRows.Select(c => c.Id).ToList();
                    var listApproved = Vacations.Where(c => listId.Contains(c.Id)).ToList();
                    listApproved.ForEach(c =>
                    {
                        c.ApprovalStatus = ApprovalStatus.Approved;
                        c.ApprovedDate = DateTime.Now;
                    });
                    var result = await OvertimeService.UpdateListAsync(listApproved);
                    if (result)
                    {
                        Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                        approvedModalVisible = false;
                        detailVisible = false;
                        await LoadDataAsync();
                    }
                    else
                    {
                        Notice.NotiError("Cập nhật dữ liệu thất bại!");
                    }
                }
                else
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                approving = false;
            }
        }

        async Task DisapprovedAsync()
        {
            try
            {
                if (selectedApproveRows.Any())
                {
                    disapproving = true;
                    StateHasChanged();
                    var listId = selectedApproveRows.Select(c => c.Id).ToList();
                    var listApproved = Vacations.Where(c => listId.Contains(c.Id)).ToList();
                    listApproved.ForEach(c =>
                    {
                        c.ApprovalStatus = ApprovalStatus.Disapproved;
                        c.ApprovedDate = DateTime.Now;
                        c.DisapprovedReason = disapprovedReason;
                    });
                    var result = await OvertimeService.UpdateListAsync(listApproved);
                    if (result)
                    {
                        Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                        approvedModalVisible = false;
                        detailVisible = false;
                        await LoadDataAsync();
                    }
                    else
                    {
                        Notice.NotiError("Cập nhật dữ liệu thất bại!");
                    }
                }
                else
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                disapproving = false;
            }
        }


        async Task CancelApproveAsync()
        {
            try
            {
                if (selectedApproveRows.Any())
                {
                    cancelApproving = true;
                    StateHasChanged();
                    var listId = selectedApproveRows.Select(c => c.Id).ToList();
                    var listApproved = Vacations.Where(c => listId.Contains(c.Id)).ToList();
                    listApproved.ForEach(c =>
                    {
                        c.ApprovalStatus = ApprovalStatus.CanceledApproved;
                        c.ApprovedDate = DateTime.Now;
                        c.DisapprovedReason = cancelApproveReason;
                    });
                    var result = await OvertimeService.UpdateListAsync(listApproved);
                    if (result)
                    {
                        Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                        cancelApproveModalVisible = false;
                        detailVisible = false;
                        await LoadDataAsync();
                    }
                    else
                    {
                        Notice.NotiError("Cập nhật dữ liệu thất bại!");
                    }
                }
                else
                {
                    Notice.NotiWarning("Không có bản ghi nào được chọn!");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cancelApproving = false;
            }
        }
    }
}
