using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingTypeEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã kiểu công")]
        public virtual string Code { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên kiểu công")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Display(Name = "Ghi chú")]
        public virtual string Note { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public Property<TimekeepingTypeEditModel> Property { get; set; } = new Property<TimekeepingTypeEditModel>();

        public TimekeepingTypeEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                 .Where(c => c != EffectiveState.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            InputFields.Add<TimekeepingTypeEditModel, EffectiveState>(c => c.EffectiveState);
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
            return Errors;
        }
    }
}
