using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using FluentNHibernate.Testing.Values;
using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.SearchModel
{
    public class PermissionGroupSearch
    {
        public Page Page { get; set; } = new Page();
        public string CodeOrName { get; set; }
        public string Code { get; set; }
        public bool? CheckExist { get; set; }
        public List<string> Ids { get; set; }
    }
}
