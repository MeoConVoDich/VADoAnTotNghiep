using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnTotNghiep.EditModel
{
    public class PermissionGroupFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();

        [Display(Name = "Tên nhóm tài khoản")]
        public virtual string CodeOrName { get; set; }

        public Property<PermissionGroupFilterEditModel> Property { get; set; } = new Property<PermissionGroupFilterEditModel>();

        public PermissionGroupFilterEditModel()
        {
        }

    }
}
