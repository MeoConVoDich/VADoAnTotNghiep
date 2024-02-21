using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class TimekeepingFormulaSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }
        public bool? CheckExist { get; set; }
    }
}
