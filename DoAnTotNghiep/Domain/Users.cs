using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Domain
{
    public class Users
    {
        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsAdmin { get; set; }
        public virtual string Permission { get; set; }
        public virtual string PermissionGroup { get; set; }
    }
}
