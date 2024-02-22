using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class VacationSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public bool? CheckExist { get; set; }
        public DateTime? StartDateCheckExist { get; set; }
        public DateTime? EndDateCheckExist { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public string UsersId { get; set; }
    }
}
