using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Config;

namespace DoAnTotNghiep.ViewModel
{
    public class UsersViewModel
    {
        public virtual string Id { get; set; }
        [Display(Name = "STT")]
        public virtual int Stt { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public virtual string UserName { get; set; }
        [Display(Name = "Tên nhân viên")]
        public virtual string Name { get; set; }
        [Display(Name = "Mã nhân viên")]
        public virtual string Code { get; set; }
        [Display(Name = "Giới tính")]
        public Gender Gender { get; set; }
        [Display(Name = "CMND/CCCD")]
        public string IdentityNumber { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneHouseholder { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Lương")]
        public int Salary { get; set; }
        public string PermissionGroup { get; set; }
        public string Permission { get; set; }
        public bool IsAdmin { get; set; }


    }
}
