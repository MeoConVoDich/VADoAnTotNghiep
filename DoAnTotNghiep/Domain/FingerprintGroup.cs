using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class FingerprintGroup
    {
        public virtual Users Users { get; set; }
        public virtual List<FingerprintManagement> FingerprintManagements { get; set; }
    }
}
