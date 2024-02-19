using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Shared
{
    public partial class TimekeepingMenu
    {
        [Inject] PermissionClaim PermissionClaim { get; set; }
    }
}
