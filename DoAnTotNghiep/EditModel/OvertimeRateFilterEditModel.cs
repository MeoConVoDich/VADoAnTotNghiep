using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class OvertimeRateFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();
        public virtual string EffectiveState { get; set; } = Config.EffectiveState.All.ToString();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Property<OvertimeRateFilterEditModel> Property { get; set; } = new Property<OvertimeRateFilterEditModel>();

        public OvertimeRateFilterEditModel()
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                .OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
        }
    }
}
