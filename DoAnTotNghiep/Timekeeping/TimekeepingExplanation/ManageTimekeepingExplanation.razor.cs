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

namespace DoAnTotNghiep.Timekeeping.TimekeepingExplanation
{
    public partial class ManageTimekeepingExplanation : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] TimekeepingExplanationService TimekeepingExplanationService { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }

        List<Domain.TimekeepingExplanation> Vacations = new List<Domain.TimekeepingExplanation>();
        List<TimekeepingExplanationViewModel> selectedApproveRows = new();
        List<TimekeepingExplanationViewModel> TimekeepingExplanationViewModels = new List<TimekeepingExplanationViewModel>();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        Table<TimekeepingExplanationViewModel> table;
        TimekeepingExplanationFilterEditModel timekeepingExplanationFilterModel = new TimekeepingExplanationFilterEditModel();
        TimekeepingExplanationDetail timekeepingExplanationDetail;
        IEnumerable<TimekeepingExplanationViewModel> selectedRows;
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
                timekeepingExplanationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
                await LoadTimekeepingTypeAsync();
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
                selectedRows = null;
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
                    timekeepingExplanationFilterModel.Page.PageIndex = e.Page;
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
                timekeepingExplanationFilterModel.Page.PageIndex = 1;
                timekeepingExplanationFilterModel.Page.PageSize = e.PageSize;
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
                timekeepingExplanationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
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

        void ViewDetail(TimekeepingExplanationViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                timekeepingExplanationDetail.LoadEditModel(data, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit(TimekeepingExplanationViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                timekeepingExplanationDetail.LoadEditModel(data, data.CreatorObject != CreatorObject.HRStaff);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(TimekeepingExplanationViewModel model = null)
        {
            try
            {
                bool result = new();
                if (model != null)
                {
                    if (model.CreatorObject == CreatorObject.HRStaff)
                    {
                        var deleteModel = Vacations.FirstOrDefault(c => c.Id == model.Id);
                        result = await TimekeepingExplanationService.DeleteAsync(deleteModel);
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
                    result = await TimekeepingExplanationService.DeleteListAsync(deleteList);
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
                Domain.TimekeepingExplanation vacation = new Domain.TimekeepingExplanation();
                vacation.ApprovalStatus = ApprovalStatus.Approved;
                vacation.CreatorObject = CreatorObject.HRStaff;
                timekeepingExplanationDetail.LoadEditModel(vacation);
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

        void ApprovedCheck(Domain.TimekeepingExplanation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(TimekeepingExplanationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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


        void DeclinedCheck(Domain.TimekeepingExplanation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(TimekeepingExplanationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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

        void CancelApproveCheck(Domain.TimekeepingExplanation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(TimekeepingExplanationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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
                    var result = await TimekeepingExplanationService.UpdateListAsync(listApproved);
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
                    var result = await TimekeepingExplanationService.UpdateListAsync(listApproved);
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
                    var result = await TimekeepingExplanationService.UpdateListAsync(listApproved);
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
