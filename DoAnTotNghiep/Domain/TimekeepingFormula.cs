using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class TimekeepingFormula
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual int CountCode { get; set; }
        public virtual string Formula { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
