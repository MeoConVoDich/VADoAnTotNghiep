using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;

namespace DoAnTotNghiep.SearchModel
{
    public class BonusDisciplineSearch
    {
        public Page Page { get; set; } = new Page();
        public string StaffCodeOrName { get; set; }
        public string CodeOrName { get; set; }
        public string Code { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual BonusDisciplineType BonusDisciplineType { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
        public bool? CheckExist { get; set; }
    }
}
