using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class UsersSearch
    {
        public Page Page { get; set; } = new Page();
        public string Code { get; set; }
        public string CodeOrName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string IdentityNumber { get; set; }
        public bool? CheckExist { get; set; }
    }
}
