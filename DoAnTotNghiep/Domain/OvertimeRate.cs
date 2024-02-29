using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class OvertimeRate
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int? Day { get; set; }
        public virtual int? Night { get; set; }
        public virtual int? DayOff { get; set; }
        public virtual int? NightOff { get; set; }
        public virtual int? DayHoliday { get; set; }
        public virtual int? NightHoliday { get; set; }
        public virtual EffectiveState EffectiveState { get; set; }
    }
}
