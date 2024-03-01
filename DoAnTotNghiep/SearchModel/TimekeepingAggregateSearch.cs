using DoAnTotNghiep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.SearchModel
{
    public class TimekeepingAggregateSearch
    {
        public string Id { get; set; }
        public Page Page { get; set; }
        public string CodeOrName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string UsersId { get; set; }
        public List<string> UsersIds { get; set; }
    }
}
