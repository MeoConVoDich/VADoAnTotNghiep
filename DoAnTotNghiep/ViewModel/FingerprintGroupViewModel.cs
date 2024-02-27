using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.ViewModel
{
    public class FingerprintGroupViewModel
    {
        public int Stt { get; set; }

        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => Users?.Code; set { } }

        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => Users?.Name; set { } }

        [Display(Name = "Ngày quét")]
        public DateTime? ScanDate { get => FingerprintManagements?.First().ScanDate; set { } }

        [Display(Name = "Vân tay")]
        public List<string> Fingerprints { get; set; }

        public UsersViewModel Users { get; set; }

        public List<FingerprintManagementViewModel> FingerprintManagements { get; set; }


        public FingerprintGroupViewModel()
        {
            Fingerprints = new();

        }

        public void GetFingerprints()
        {
            if (FingerprintManagements?.Any() == true)
            {
                Fingerprints = FingerprintManagements.Select(c => c.ScanDate.Value.TimeOfDay)
                    .Select(c => c.ToString()).ToList();
            }
        }
    }
}
