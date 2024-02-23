using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class OvertimeSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public bool? CheckExist { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public string UsersId { get; set; }
    }
}
