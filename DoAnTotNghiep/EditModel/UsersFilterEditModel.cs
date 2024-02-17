using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class UsersFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();
        [Display(Name = "Mã nhân viên")]
        public string Code { get; set; }
        [Display(Name = "Họ và tên")]
        public virtual string Name { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public virtual string UserName { get; set; }
        public string CodeOrName { get; set; }

    }
}
