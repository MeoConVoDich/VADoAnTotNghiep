using System.ComponentModel.DataAnnotations;

namespace DoAnTotNghiep.ViewModel
{
    public class PermissionGroupViewModel
    {
        public virtual string Id { get; set; }
        public virtual int Stt { get; set; }

        [Display(Name = "Mã nhóm")]
        public virtual string CodeGroup { get; set; }

        [Display(Name = "Tên nhóm")]
        public virtual string NameGroup { get; set; }

        [Display(Name = "Ghi chú")]
        public virtual string Note { get; set; }

        public virtual string Permission { get; set; }
    }

    public class ClaimViewModel
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
