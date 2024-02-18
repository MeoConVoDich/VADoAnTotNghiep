using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DoAnTotNghiep.EditModel
{
    public class BonusDisciplineFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();
        public string StaffCodeOrName { get; set; }
        public string CodeOrName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public virtual BonusDisciplineType BonusDisciplineType { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }

        public Property<BonusDisciplineFilterEditModel> Property { get; set; } = new Property<BonusDisciplineFilterEditModel>();

        public BonusDisciplineFilterEditModel()
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                .OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.BonusDisciplineType)] = Enum.GetValues(typeof(BonusDisciplineType)).Cast<BonusDisciplineType>()
                .OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
        }
    }
}
