using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Timekeeping
{
    public partial class SetupTimekeeping : ComponentBase
    {
        [Inject] PermissionClaim PermissionClaim { get; set; }
        string activeKey;
    }
}
