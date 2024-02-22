using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Profile
{
    public partial class RegisterApplication : ComponentBase
    {
        [Inject] PermissionClaim PermissionClaim { get; set; }
        string activeKey;
    }
}
