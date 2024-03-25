using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CreateUsers : EditBaseModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [MaxLength(255, ErrorMessage = "{0} không được dài hơn {1} ký tự")]
        public string Name { get; set; }

        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [MaxLength(20, ErrorMessage = "{0} không được dài hơn {1} ký tự")]
        public string Code { get; set; }

        public Property<CreateUsers> Property { get; set; } = new Property<CreateUsers>();

        public CreateUsers(bool isEdit = true)
        {
        }
    }

    public class UsersEditModel : EditBaseModel
    {
        public string Id { get; set; }

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [MaxLength(255, ErrorMessage = "{0} không được dài hơn {1} ký tự")]
        public string Name { get; set; }

        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [MaxLength(20, ErrorMessage = "{0} không được dài hơn {1} ký tự")]
        public string Code { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Giới tính")]
        public Gender Gender { get; set; }

        [Display(Name = "CMND/CCCD")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        public string IdentityNumber { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneHouseholder { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Lương")]
        public int? Salary { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Permission { get; set; }
        public string PermissionGroup { get; set; }

        public Property<UsersEditModel> Property { get; set; } = new Property<UsersEditModel>();

        public UsersEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.Gender)] = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                .Where(c => c != Gender.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
        }
    }
}
