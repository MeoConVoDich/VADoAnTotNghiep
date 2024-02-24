using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingExplanationFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public virtual string ApprovalStatus { get; set; } = Config.ApprovalStatus.All.ToString();
        public virtual string Year { get; set; } = DateTime.Now.Year.ToString();
        public virtual string Month { get; set; } = DateTime.Now.Month.ToString();
        public virtual DateTime? SearchMonth { get; set; } = DateTime.Now;
        public virtual string UsersId { get; set; }
        public Property<TimekeepingExplanationFilterEditModel> Property { get; set; } = new Property<TimekeepingExplanationFilterEditModel>();

        public TimekeepingExplanationFilterEditModel()
        {
            DataSource[Property.NameProperty(c => c.ApprovalStatus)] = Enum.GetValues(typeof(ApprovalStatus)).Cast<ApprovalStatus>()
                .OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.Name(c => c.Year)] = DateTimeExtentions.GetListYears();
            DataSource[Property.Name(c => c.Month)] = DateTimeExtentions.GetListMonths();
        }
    }
}
