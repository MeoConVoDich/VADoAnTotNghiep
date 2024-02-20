using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class TimekeepingType
    {
        public virtual string Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Note { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string CodeName { get => $"{Code} - {Name}"; set { } }
    }
}
