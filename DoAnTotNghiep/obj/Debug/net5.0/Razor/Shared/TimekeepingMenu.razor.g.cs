#pragma checksum "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1c7b73d075ccaf229cbde383caf50dc67ffc7362"
// <auto-generated/>
#pragma warning disable 1591
namespace DoAnTotNghiep.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.EditModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Domain;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.ViewModel;

#line default
#line hidden
#nullable disable
    public partial class TimekeepingMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 1 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
 if (PermissionClaim.IsTimekeepingMenuVisible())
{

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<AntDesign.Menu>(0);
            __builder.AddAttribute(1, "Theme", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.MenuTheme>(
#nullable restore
#line 3 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
             MenuTheme.Dark

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Class", "main_menu");
            __builder.AddAttribute(3, "Mode", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.MenuMode>(
#nullable restore
#line 3 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                                                     MenuMode.Horizontal

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "DefaultSelectedKeys", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<System.String>>(
#nullable restore
#line 4 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                            new[]{"/cham-cong/xep-ca-lam-viec"}

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "TriggerSubMenuAction", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.Trigger>(
#nullable restore
#line 4 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                                                                                        Trigger.Hover

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 7 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
     if (PermissionClaim?.TIMEKEEPINGSHIFTSTAFF_VIEW == true)
    {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(7);
                __builder2.AddAttribute(8, "Key", "/cham-cong/xep-ca-lam-viec");
                __builder2.AddAttribute(9, "RouterLink", "/cham-cong/xep-ca-lam-viec");
                __builder2.AddAttribute(10, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 10 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(11, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(12, "\r\n            Bảng ca làm việc\r\n        ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 13 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
     if (PermissionClaim?.FINGERPRINT_VIEW == true)
    {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(13);
                __builder2.AddAttribute(14, "Key", "/cham-cong/du-lieu-vao-ra");
                __builder2.AddAttribute(15, "RouterLink", "/cham-cong/du-lieu-vao-ra");
                __builder2.AddAttribute(16, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 17 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(18, "\r\n            Dữ liệu vào ra\r\n        ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 20 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
     if (PermissionClaim.IsTimekeepingManagementMenuVisible())
    {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(19);
                __builder2.AddAttribute(20, "Key", "/cham-cong/quan-ly-don");
                __builder2.AddAttribute(21, "RouterLink", "/cham-cong/quan-ly-don");
                __builder2.AddAttribute(22, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 24 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(23, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(24, "\r\n            Quản lý đơn\r\n        ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 27 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
     if (PermissionClaim?.TIMEKEEPINGAGGREGATE_VIEW == true)
    {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(25);
                __builder2.AddAttribute(26, "Key", "/cham-cong/xu-ly-du-lieu-lam-them");
                __builder2.AddAttribute(27, "RouterLink", "/cham-cong/xu-ly-du-lieu-lam-them");
                __builder2.AddAttribute(28, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 31 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(29, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(30, "\r\n            Xử lý dữ liệu làm thêm\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(31, "\r\n        ");
                __builder2.OpenComponent<AntDesign.MenuItem>(32);
                __builder2.AddAttribute(33, "Key", "/cham-cong/tong-hop-cong-may");
                __builder2.AddAttribute(34, "RouterLink", "/cham-cong/tong-hop-cong-may");
                __builder2.AddAttribute(35, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 35 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(36, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(37, "\r\n            Tổng hợp công máy\r\n        ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 38 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
     if (PermissionClaim?.SETUP_TIMEKEEPING == true)
    {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(38);
                __builder2.AddAttribute(39, "Key", "/cham-cong/quy-dinh-cham-cong");
                __builder2.AddAttribute(40, "RouterLink", "/cham-cong/quy-dinh-cham-cong");
                __builder2.AddAttribute(41, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 42 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(42, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(43, "\r\n            Quy định chấm công\r\n        ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 45 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
    }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 47 "C:\Users\Win10Pro\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\TimekeepingMenu.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
