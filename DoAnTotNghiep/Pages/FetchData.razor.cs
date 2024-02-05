using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Data;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.IService;
using DoAnTotNghiep.Service;

namespace DoAnTotNghiep.Pages
{
    public partial class FetchData : ComponentBase
    {
        [Inject] UsersService UsersService { get; set; }
        List<Users> ListUsers = new List<Users>();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await UsersService.GetAllWithFilterAsync();
                ListUsers = result.Item1;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
