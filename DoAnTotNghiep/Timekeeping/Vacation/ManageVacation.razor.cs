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

namespace DoAnTotNghiep.Timekeeping.Vacation
{
    public partial class ManageVacation : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] VacationService VacationService { get; set; }
        [Inject] TimekeepingTypeService TimekeepingTypeService { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        List<Domain.Vacation> Vacations = new List<Domain.Vacation>();
        List<VacationViewModel> selectedApproveRows = new();
        List<TimekeepingType> TimekeepingTypes = new List<TimekeepingType>();
        List<VacationViewModel> VacationViewModels = new List<VacationViewModel>();
        Table<VacationViewModel> table;
        VacationFilterEditModel vacationFilterModel = new VacationFilterEditModel();
        VacationDetail vacationDetail;
        IEnumerable<VacationViewModel> selectedRows;
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
                vacationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
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
                var search = Mapper.Map<VacationSearch>(vacationFilterModel);
                var page = await VacationService.GetPageWithFilterAsync(search);
                Vacations = page.Item1 ?? new List<Domain.Vacation>();
                VacationViewModels = Mapper.Map<List<VacationViewModel>>(Vacations ?? new List<Domain.Vacation>());
                int stt = vacationFilterModel.Page.PageSize * (vacationFilterModel.Page.PageIndex - 1) + 1;
                VacationViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                    c.TimekeepingTypeName = TimekeepingTypes.FirstOrDefault(v => v.Id == c.TimekeepingTypeId)?.CodeName;
                });
                vacationFilterModel.Page.Total = page.Item2;
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

        void OnRowClick(RowData<VacationViewModel> rowData)
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
                    vacationFilterModel.Page.PageIndex = e.Page;
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
                vacationFilterModel.Page.PageIndex = 1;
                vacationFilterModel.Page.PageSize = e.PageSize;
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
                vacationFilterModel.Page = new Page() { PageIndex = 1, PageSize = 15 };
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
                vacationFilterModel.ApprovalStatus = approvalStatus;
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
                vacationFilterModel.Year = year;
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
                vacationFilterModel.Month = month;
                await LoadDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void ViewDetail(VacationViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                vacationDetail.LoadEditModel(data, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit(VacationViewModel model)
        {
            try
            {
                detailVisible = true;
                var data = Vacations.FirstOrDefault(c => c.Id == model.Id);
                vacationDetail.LoadEditModel(data, data.CreatorObject != CreatorObject.HRStaff);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task DeleteAsync(VacationViewModel model = null)
        {
            try
            {
                bool result = new();
                if (model != null)
                {
                    if (model.CreatorObject == CreatorObject.HRStaff)
                    {
                        var deleteModel = Vacations.FirstOrDefault(c => c.Id == model.Id);
                        result = await VacationService.DeleteAsync(deleteModel);
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
                    result = await VacationService.DeleteListAsync(deleteList);
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
                Domain.Vacation vacation = new Domain.Vacation();
                vacation.ApprovalStatus = ApprovalStatus.Approved;
                vacation.CreatorObject = CreatorObject.HRStaff;
                vacationDetail.LoadEditModel(vacation);
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

        void ApprovedCheck(Domain.Vacation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(VacationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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


        void DeclinedCheck(Domain.Vacation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(VacationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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

        void CancelApproveCheck(Domain.Vacation selected = null)
        {
            try
            {
                selectedApproveRows.Clear();
                if (selected != null)
                {
                    selectedApproveRows.Add(VacationViewModels.FirstOrDefault(c => c.Id == selected.Id));
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
                    var result = await VacationService.UpdateListAsync(listApproved);
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
                    var result = await VacationService.UpdateListAsync(listApproved);
                    if (result)
                    {
                        Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                        detailVisible = false;
                        disapprovedModalVisible = false;
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
                    var result = await VacationService.UpdateListAsync(listApproved);
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
