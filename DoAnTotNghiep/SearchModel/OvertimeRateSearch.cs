using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class OvertimeRateSearch
    {
        public Page Page { get; set; } = new Page();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
    }
}
