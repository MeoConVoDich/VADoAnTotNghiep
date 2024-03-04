using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingAggregateFilterEditModel : EditBaseModel
    {
        public Page Page { get; set; }
        public string Id { get; set; }

        [Display(Name = "Năm")]
        public string Year { get; set; } = DateTime.Now.Year.ToString();

        [Display(Name = "Tháng")]
        public string Month { get; set; } = DateTime.Now.Month.ToString();

        public string UsersId { get; set; }

        public string CodeOrName { get; set; }
        public Property<TimekeepingAggregateFilterEditModel> Property  = new Property<TimekeepingAggregateFilterEditModel>();
        public TimekeepingAggregateFilterEditModel()
        {
            DataSource[Property.Name(c => c.Year)] = DateTimeExtentions.GetListYears();
            DataSource[Property.Name(c => c.Month)] = DateTimeExtentions.GetListMonths();
        }
    }
}
