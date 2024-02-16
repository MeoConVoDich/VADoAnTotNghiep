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
        public string Password { get; set; }
    }

    public class UsersEditModel
    {
        public string Id { get; set; }

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [MaxLength(255, ErrorMessage = "{0} không được dài hơn {0} ký tự")]
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
        public int Salary { get; set; }
    }
}
