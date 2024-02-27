using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class FingerprintManagement
    {
        public virtual string Id { get; set; }
        public virtual string UsersId { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime? ScanDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual Users Users { get; set; }
    }
}
