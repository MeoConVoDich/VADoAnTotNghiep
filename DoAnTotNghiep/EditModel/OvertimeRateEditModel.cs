using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class OvertimeRateEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }
        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ngày hiệu lực")]
        public virtual DateTime Date { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ngày (%)")]
        public virtual int Day { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Đêm (%)")]
        public virtual int Night { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public Property<OvertimeRateEditModel> Property { get; set; } = new Property<OvertimeRateEditModel>();

        public OvertimeRateEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                 .Where(c => c != EffectiveState.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            InputFields.Add<OvertimeRateEditModel, EffectiveState>(c => c.EffectiveState);
            InputFields.Add<OvertimeRateEditModel>(c => c.Day);
            InputFields.Add<OvertimeRateEditModel>(c => c.Night);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.EffectiveState))
            {
                if (EffectiveState == EffectiveState.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.Day))
            {
                if (Day == 0)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu phải lớn hơn 0!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.Night))
            {
                if (Night == 0)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu phải lớn hơn 0!");
                }
            }
            return Errors;
        }
    }
}
