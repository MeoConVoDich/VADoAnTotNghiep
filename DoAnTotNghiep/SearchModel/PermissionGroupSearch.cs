using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using System;

namespace DoAnTotNghiep.SearchModel
{
    public class PermissionGroupSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public string Code { get; set; }
        public bool? CheckExist { get; set; }
    }
}
