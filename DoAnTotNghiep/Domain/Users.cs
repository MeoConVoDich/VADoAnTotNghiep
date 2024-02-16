using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual string IdentityNumber { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string PhoneHouseholder { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual int Salary { get; set; }
    }
}
