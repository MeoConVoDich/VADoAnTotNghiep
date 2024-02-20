using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class TimekeepingTypeSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
        public bool? CheckExist { get; set; }
    }
}
