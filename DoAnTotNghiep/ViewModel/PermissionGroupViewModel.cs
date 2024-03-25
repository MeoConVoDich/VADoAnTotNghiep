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
    }
}
