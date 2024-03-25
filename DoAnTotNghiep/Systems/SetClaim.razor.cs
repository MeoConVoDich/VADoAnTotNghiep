using AntDesign;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using DoAnTotNghiep.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Systems
{
    public partial class SetClaim : ComponentBase
    {
        [Inject] PermissionGroupService PermissionGroupService { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Parameter] public SetClaimType SetClaimType { get; set; } = SetClaimType.PermissionGroup;
        [Parameter] public EventCallback ValueChanged { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        List<ClaimViewModel> claimTemplates;
        List<IGrouping<string, ClaimViewModel>> claimViewModels;
        List<IGrouping<string, ClaimViewModel>> claimTimekeepingViewModels = new();
        List<IGrouping<string, ClaimViewModel>> claimSystemViewModels = new();
        List<IGrouping<string, ClaimViewModel>> claimSalaryViewModels = new();
        List<IGrouping<string, ClaimViewModel>> claimStaffViewModels = new();
        List<string> types = new List<string>();
        Tree<string> treeOne;
        Tree<string> treeTwo;
        Tree<string> treeThree;
        Tree<string> treeFour;
        string typeSystem = "Hệ thống";
        string typeStaff = "Nhân sự";
        string typeTimekeeping = "Chấm công";
        string typeSalary = "Tiền lương";
        bool isAdmin = false;
        string idObject;
        string searchKey;

        public void LoadClaim(List<string> permissions, bool admin, string id)
        {
            try
            {
                idObject = id;
                isAdmin = admin;
                claimTemplates = new List<ClaimViewModel>
                {
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_VIEW, Group = "Quản lý tài khoản", Name ="Xem danh sách", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_ADD, Group = "Quản lý tài khoản", Name ="Thêm mới", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_EDIT, Group = "Quản lý tài khoản", Name ="Chỉnh sửa", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_DELETE, Group = "Quản lý tài khoản", Name ="Xóa", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_SETROLE, Group = "Quản lý tài khoản", Name ="Gán nhóm tài khoản", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_SETCLAIM, Group = "Quản lý tài khoản", Name ="Phân quyền tài khoản", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ACCOUNT_CHANGEPASSWORD, Group = "Quản lý tài khoản",Name = "Đổi mật khẩu", Type = typeSystem},

                    new ClaimViewModel{ Id = PermissionKey.ROLE_VIEW, Group = "Quản lý nhóm tài khoản", Name ="Xem danh sách", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ROLE_ADD, Group = "Quản lý nhóm tài khoản", Name ="Thêm mới", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ROLE_EDIT, Group = "Quản lý nhóm tài khoản", Name ="Chỉnh sửa", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ROLE_DELETE, Group = "Quản lý nhóm tài khoản", Name ="Xóa", Type = typeSystem},
                    new ClaimViewModel{ Id = PermissionKey.ROLE_SETCLAIM, Group = "Quản lý nhóm tài khoản", Name ="Phân quyền", Type = typeSystem},

                    new ClaimViewModel{ Id = PermissionKey.STAFFPROFILE_VIEW, Group = "Quản lý hồ sơ nhân viên", Name ="Xem danh sách toàn đơn vị", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFPROFILE_ADD, Group = "Quản lý hồ sơ nhân viên", Name ="Thêm mới", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFPROFILE_EDIT, Group = "Quản lý hồ sơ nhân viên", Name ="Chỉnh sửa", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFPROFILE_DELETE, Group = "Quản lý hồ sơ nhân viên", Name ="Xóa", Type = typeStaff},

                    new ClaimViewModel{ Id = PermissionKey.DISCIPLINE_BONUS_VIEW, Group = "Quản lý quyết định", Name ="Xem danh sách", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.DISCIPLINE_BONUS_ADD, Group = "Quản lý quyết định", Name ="Thêm mới", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.DISCIPLINE_BONUS_EDIT, Group = "Quản lý quyết định", Name ="Chỉnh sửa", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.DISCIPLINE_BONUS_DELETE, Group = "Quản lý quyết định", Name ="Xóa", Type = typeStaff},

                    new ClaimViewModel{ Id = PermissionKey.STAFFRELATIONSHIP_VIEW, Group = "Quản lý quan hệ nhân thân", Name ="Xem danh sách", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFRELATIONSHIP_ADD, Group = "Quản lý quan hệ nhân thân", Name ="Thêm mới", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFRELATIONSHIP_EDIT, Group = "Quản lý quan hệ nhân thân", Name ="Chỉnh sửa", Type = typeStaff},
                    new ClaimViewModel{ Id = PermissionKey.STAFFRELATIONSHIP_DELETE, Group = "Quản lý quan hệ nhân thân", Name ="Xóa", Type = typeStaff},

                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGSHIFTSTAFF_VIEW, Group = "Quản lý xếp ca làm việc", Name ="Xem toàn đơn vị", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGSHIFTSTAFF_EDIT, Group ="Quản lý xếp ca làm việc" , Name ="Chỉnh sửa", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGSHIFTSTAFF_DELETE, Group = "Quản lý xếp ca làm việc", Name  ="Xóa", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.FINGERPRINT_VIEW, Group = "Quản lý dữ liệu vào ra", Name ="Xem danh sách", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.FINGERPRINT_ADD, Group = "Quản lý dữ liệu vào ra", Name ="Thêm mới", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.MANAGEVACATIONREGISTRATION_VIEW, Group = "Quản lý đăng ký nghỉ", Name ="Xem danh sách", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEVACATIONREGISTRATION_ADD, Group = "Quản lý đăng ký nghỉ", Name ="Thêm mới", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEVACATIONREGISTRATION_EDIT, Group = "Quản lý đăng ký nghỉ", Name ="Chỉnh sửa", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEVACATIONREGISTRATION_DELETE, Group = "Quản lý đăng ký nghỉ", Name ="Xóa", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.MANAGEOVERTIMEREGISTER_VIEW, Group = "Quản lý đăng ký làm thêm giờ", Name ="Xem danh sách", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEOVERTIMEREGISTER_ADD, Group = "Quản lý đăng ký làm thêm giờ", Name ="Chỉnh sửa", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEOVERTIMEREGISTER_EDIT, Group = "Quản lý đăng ký làm thêm giờ", Name ="Thêm mới", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGEOVERTIMEREGISTER_DELETE, Group = "Quản lý đăng ký làm thêm giờ", Name ="Xóa", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.MANAGETIMEKEEPINGEXPLANATION_VIEW, Group = "Quản lý giải trình chấm công", Name ="Xem danh sách", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGETIMEKEEPINGEXPLANATION_ADD, Group ="Quản lý giải trình chấm công" , Name ="Phê duyệt yêu cầu", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGETIMEKEEPINGEXPLANATION_EDIT, Group ="Quản lý giải trình chấm công" , Name ="Phê duyệt yêu cầu", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.MANAGETIMEKEEPINGEXPLANATION_DELETE, Group ="Quản lý giải trình chấm công" , Name ="Phê duyệt yêu cầu", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGAGGREGATE_VIEW, Group = "Tổng hợp dữ liệu công", Name ="Xem danh sách", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGAGGREGATE_DELETE, Group = "Tổng hợp dữ liệu công", Name ="Xóa", Type = typeTimekeeping},
                    new ClaimViewModel{ Id = PermissionKey.TIMEKEEPINGAGGREGATE, Group = "Tổng hợp dữ liệu công", Name ="Tổng hợp công theo toàn đơn vị", Type = typeTimekeeping},

                    new ClaimViewModel{ Id = PermissionKey.SETUP_TIMEKEEPING, Group = "Thiết lập", Name ="Thiết lập dữ liệu dùng chung", Type = typeTimekeeping},
                };
                claimTemplates.ForEach(c =>
                {
                    if (permissions.Contains(c.Id) || isAdmin)
                    {
                        c.Checked = true;
                    }
                });
                claimTimekeepingViewModels.Clear();
                claimStaffViewModels.Clear();
                claimSalaryViewModels.Clear();
                claimSystemViewModels.Clear();
                types = claimTemplates.Select(c => c.Type).ToHashSet().ToList();
                claimViewModels = claimTemplates.GroupBy(c => c.Group).ToList();
                claimTimekeepingViewModels = claimTemplates.Where(c => c.Type == typeTimekeeping).GroupBy(c => c.Group).ToList();
                claimStaffViewModels = claimTemplates.Where(c => c.Type == typeStaff).GroupBy(c => c.Group).ToList();
                claimSalaryViewModels = claimTemplates.Where(c => c.Type == typeSalary).GroupBy(c => c.Group).ToList();
                claimSystemViewModels = claimTemplates.Where(c => c.Type == typeSystem).GroupBy(c => c.Group).ToList();

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        protected async Task HandleAddRoleValidSubmitAsync()
        {
            try
            {
                bool result;
                var changedClaims = claimTemplates.Where(c => c.Checked).Select(c => c.Id).ToList();
                if (SetClaimType == SetClaimType.PermissionGroup)
                {
                    result = await PermissionGroupService.UpdatePermissionAsync(idObject, changedClaims);
                }
                else
                {
                    result = await UsersService.UpdatePermissionAsync(idObject, changedClaims);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật thành công!");
                    await ValueChanged.InvokeAsync(null);
                }
                else
                {
                    Notice.NotiError("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Search(string search)
        {
            try
            {
                searchKey = search;
                if (searchKey.IsNotNullOrEmpty())
                {
                    treeOne.ExpandAll();
                    treeTwo.ExpandAll();
                    treeThree.ExpandAll();
                    treeFour.ExpandAll();
                }
                else
                {
                    treeOne.CollapseAll();
                    treeTwo.CollapseAll();
                    treeThree.CollapseAll();
                    treeFour.CollapseAll();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        void OnCheckBoxChanged(TreeEventArgs<string> e)
        {
            try
            {
                if (isAdmin)
                {
                    return;
                }
                if (e.Node.ParentNode == null)
                {
                    claimTemplates.Where(c => c.Type == e.Node.Title).ForEach(c => c.Checked = e.Node.Checked);
                }
                else if (types.Contains(e.Node.ParentNode.Title))
                {
                    var group = claimViewModels.FirstOrDefault(c => c.Key == e.Node.Title);
                    if (group != null)
                    {
                        claimTemplates.Where(c => c.Group == group.Key).ForEach(c => c.Checked = e.Node.Checked);
                    }
                }
                else
                {
                    claimTemplates.FirstOrDefault(c => c.Id == e.Node.Key).Checked = e.Node.Checked;
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        void CheckAllChanged(bool value)
        {
            try
            {
                if (isAdmin)
                {
                    return;
                }
                claimTemplates.ForEach(c => c.Checked = value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
