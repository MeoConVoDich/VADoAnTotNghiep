using AntDesign;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] Blazored.SessionStorage.ISessionStorageService sessionStorage { get; set; }
        [Inject] NotificationService Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }

        LoginModel login = new LoginModel();

        public async Task<bool> LoginAsync()
        {
            var tk = await UsersService.GetUsersAsync(login.UserName, login.Password);
            if (tk != null)
            {
                NavigationManager.NavigateTo("/index");
                await sessionStorage.SetItemAsync("tenDangNhap", tk.UserName);
                return await Task.FromResult(true);
            }
            else
            {
                await Notice.Open(new NotificationConfig()
                {
                    Message = "Thông báo",
                    Description = "Tài khoản hoặc mật khẩu không đúng"
                });
                return await Task.FromResult(false);
            }
        }
    }
}
