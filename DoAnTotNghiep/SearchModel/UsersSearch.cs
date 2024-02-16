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
        public virtual string Name { get; set; }
        public virtual string UserName { get; set; }
    }
}
