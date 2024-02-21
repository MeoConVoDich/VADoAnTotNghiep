using DoAnTotNghiep.Components;
using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingFormulaFilterEditModel
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public Property<TimekeepingFormulaFilterEditModel> Property { get; set; } = new Property<TimekeepingFormulaFilterEditModel>();

        public TimekeepingFormulaFilterEditModel()
        {
        }
    }
}
