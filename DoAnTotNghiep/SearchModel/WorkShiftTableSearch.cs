using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class WorkShiftTableSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string UsersId { get; set; }
    }
}
