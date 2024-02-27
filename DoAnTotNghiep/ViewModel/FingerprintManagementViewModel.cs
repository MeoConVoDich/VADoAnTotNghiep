using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class FingerprintManagementViewModel
    {
        public virtual string Id { get; set; }
        public int Stt { get; set; }
        public virtual string UsersId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }

        [Display(Name = "Ngày quét")]
        public DateTime? ScanDate { get; set; }

        [Display(Name = "Vân tay")]
        public string Fingerprint { get; set; }
    }
}
