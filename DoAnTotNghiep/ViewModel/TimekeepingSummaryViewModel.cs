using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class TimekeepingSummaryViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public TimekeepingSummaryViewModel()
        {

        }

        public TimekeepingSummaryViewModel(string code, string name, string value)
        {
            Code = code;
            Name = name;
            Value = value;
        }
    }
}
