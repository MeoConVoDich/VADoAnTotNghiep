using Blazored.SessionStorage;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Staff;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Profile
{
    public partial class StaffProfilePersonal : ComponentBase
    {
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Inject] UsersService UsersService { get; set; }

        StaffProfileDetail staffProfileDetail;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                var users = (await UsersService.GetAllWithFilterAsync(new UsersSearch() { Id = usersId })).Item1?.FirstOrDefault();
                if (users != null)
                {
                    await staffProfileDetail.LoadEditModelAsync(users, true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
