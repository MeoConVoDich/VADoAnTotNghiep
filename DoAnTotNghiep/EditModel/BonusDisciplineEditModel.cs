using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DoAnTotNghiep.EditModel
{
    public class BonusDisciplineEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        public virtual string UsersId { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên quyết định")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Số quyết định")]
        public virtual string Code { get; set; }

        [Display(Name = "Tệp đính kèm")]
        public virtual string PathAttachFile { get; set; }
        public virtual string AttachFileName { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Loại quyết định")]
        public virtual BonusDisciplineType BonusDisciplineType { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Số tiền")]
        public virtual int? Amount { get; set; }
        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }
        public virtual DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ngày hiệu lực")]
        public virtual DateTime Date { get; set; }

        public Property<BonusDisciplineEditModel> Property { get; set; } = new Property<BonusDisciplineEditModel>();

        public BonusDisciplineEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                 .Where(c => c != EffectiveState.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.BonusDisciplineType)] = Enum.GetValues(typeof(BonusDisciplineType)).Cast<BonusDisciplineType>()
                .Where(c => c != BonusDisciplineType.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
        }
    }
}
