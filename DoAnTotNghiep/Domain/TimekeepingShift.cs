using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class TimekeepingShift
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual DateTime? StartBreaksTime { get; set; }
        public virtual DateTime? EndBreaksTime { get; set; }
        public virtual decimal? Duration { get; set; }
        public virtual string TimekeepingTypeFull { get; set; }
        public virtual string TimekeepingTypeFirst { get; set; }
        public virtual string TimekeepingTypeSecond { get; set; }
        public virtual string TimekeepingTypeOff { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
        public virtual BreaksTimeType BreaksTimeType { get; set; }
        public virtual string CodeName { get => $"{Code} - {Name}"; set { } }
    }
}
