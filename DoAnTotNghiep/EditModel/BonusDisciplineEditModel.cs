using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DoAnTotNghiep.EditModel
{
    public class BonusDisciplineEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        public virtual string UsersId { get => UsersData?.Id; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => UsersData?.Name; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => UsersData?.Code; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên quyết định")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Số quyết định")]
        public virtual string Code { get; set; }

        [Display(Name = "Tệp đính kèm")]
        public virtual string PathAttachFile { get; set; }
        [Display(Name = "Tệp đính kèm")]
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
        public virtual UsersEditModel UsersData { get; set; } = new UsersEditModel();

        public Property<BonusDisciplineEditModel> Property { get; set; } = new Property<BonusDisciplineEditModel>();

        public BonusDisciplineEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                 .Where(c => c != EffectiveState.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.BonusDisciplineType)] = Enum.GetValues(typeof(BonusDisciplineType)).Cast<BonusDisciplineType>()
                .Where(c => c != BonusDisciplineType.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));

            InputFields.Add<BonusDisciplineEditModel, EffectiveState>(c => c.EffectiveState);
            InputFields.Add<BonusDisciplineEditModel, BonusDisciplineType>(c => c.BonusDisciplineType);
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

            if (nameProperty == Property.NameProperty(c => c.BonusDisciplineType))
            {
                if (BonusDisciplineType == BonusDisciplineType.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }

            return Errors;
        }
    }
}
