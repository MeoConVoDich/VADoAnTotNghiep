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
using DoAnTotNghiep.Config;
using System.Security.Claims;
using DoAnTotNghiep.Shared;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using NHibernate.Mapping;

namespace DoAnTotNghiep.Pages
{
    public partial class DangNhap : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Inject] NotificationService Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }

        LoginModel login = new LoginModel();

        public async Task<bool> LoginAsync()
        {
            try
            {
                var tk = await UsersService.GetUsersAsync(login.UserName, login.Password);
                if (tk != null)
                {
                    ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthienticated(login.UserName);
                    await sessionStorage.SetItemAsync("tenDangNhap", tk.UserName);
                    await sessionStorage.SetItemAsync("UsersId", tk.Id);
                    var claims = new List<string>();
                    if (tk.IsAdmin)
                    {
                        claims.Add("Admin");
                    }
                    else
                    {
                        List<string> claimUsers = new List<string>();
                        List<string> permissioGroups = new List<string>();
                        if (tk.PermissionGroup.IsNotNullOrEmpty())
                        {
                            permissioGroups = JsonSerializer.Deserialize<List<string>>(tk.PermissionGroup);
                        }
                        if (tk.Permission.IsNotNullOrEmpty())
                        {
                            claimUsers = JsonSerializer.Deserialize<List<string>>(tk.Permission);
                        }
                        claims.AddRange(claimUsers);
                    }
                    PermissionClaim.Claims(claims);
                    NavigationManager.NavigateTo("/ca-nhan/thong-tin-ca-nhan");
                    return await Task.FromResult(true);
                }
                else
                {
                    var users = await UsersService.GetUsersByUserNameAsync(login.UserName);
                    if (users != null)
                    {
                        await Notice.Open(new NotificationConfig()
                        {
                            NotificationType = NotificationType.Error,
                            Message = "Thông báo",
                            Description = "Mật khẩu không đúng!"
                        });
                    }
                    else
                    {
                        await Notice.Open(new NotificationConfig()
                        {
                            NotificationType = NotificationType.Error,
                            Message = "Thông báo",
                            Description = "Tài khoản không tồn tại"
                        });
                    }
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                await Notice.Open(new NotificationConfig()
                {
                    NotificationType = NotificationType.Error,
                    Message = "Thông báo",
                    Description = "Tài khoản hoặc mật khẩu không đúng"
                });
                return await Task.FromResult(false);
            }
        }
    }
}
