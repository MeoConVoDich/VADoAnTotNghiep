using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnTotNghiep.EditModel
{
    public class PermissionGroupEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên nhóm tài khoản")]
        public virtual string NameGroup { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã nhóm tài khoản")]
        public virtual string CodeGroup { get; set; }

        [Display(Name = "Ghi chú")]
        public virtual string Note { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public Property<PermissionGroupEditModel> Property { get; set; } = new Property<PermissionGroupEditModel>();

        public PermissionGroupEditModel(bool isEdit = true)
        {
        }
    }
}
