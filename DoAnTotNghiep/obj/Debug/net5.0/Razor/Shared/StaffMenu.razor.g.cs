#pragma checksum "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "265935cdd7121b0d107940f4e0e9cfb2d38491b55959617fb899e7ac243e19b1"
// <auto-generated/>
#pragma warning disable 1591
namespace DoAnTotNghiep.Shared
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Config;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.EditModel;

#line default
#line hidden
#nullable disable
    public partial class StaffMenu : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::AntDesign.Menu>(0);
            __builder.AddAttribute(1, "Theme", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::AntDesign.MenuTheme>(
#nullable restore
#line 1 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
             MenuTheme.Dark

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(2, "Class", (object)("main_menu"));
            __builder.AddAttribute(3, "Mode", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::AntDesign.MenuMode>(
#nullable restore
#line 1 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                                                     MenuMode.Horizontal

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(4, "DefaultSelectedKeys", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Collections.Generic.IEnumerable<System.String>>(
#nullable restore
#line 2 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                            new[]{"/ca-nhan/ho-so-ca-nhan"}

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(5, "TriggerSubMenuAction", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::AntDesign.Trigger>(
#nullable restore
#line 2 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                                                                                    Trigger.Hover

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(6, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<global::AntDesign.MenuItem>(7);
                __builder2.AddAttribute(8, "Key", (object)("/ca-nhan/ho-so-ca-nhan"));
                __builder2.AddAttribute(9, "RouterLink", (object)("/ca-nhan/ho-so-ca-nhan"));
                __builder2.AddAttribute(10, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 3 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                                                                                            NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(11, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(12, "\r\n        Hồ sơ\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(13, "\r\n    ");
                __builder2.OpenComponent<global::AntDesign.MenuItem>(14);
                __builder2.AddAttribute(15, "Key", (object)("/ca-nhan/bang-cong-ca-nhan"));
                __builder2.AddAttribute(16, "RouterLink", (object)("/ca-nhan/bang-cong-ca-nhan"));
                __builder2.AddAttribute(17, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 7 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                           NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(18, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(19, "\r\n        Bảng công\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(20, "\r\n    ");
                __builder2.OpenComponent<global::AntDesign.MenuItem>(21);
                __builder2.AddAttribute(22, "Key", (object)("/ca-nhan/phieu-luong"));
                __builder2.AddAttribute(23, "RouterLink", (object)("/ca-nhan/phieu-luong"));
                __builder2.AddAttribute(24, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 11 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                           NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(25, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(26, "\r\n        Phiếu lương\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(27, "\r\n    ");
                __builder2.OpenComponent<global::AntDesign.MenuItem>(28);
                __builder2.AddAttribute(29, "Key", (object)("/ca-nhan/dang-ky/dang-ky-nghi-ca-nhan"));
                __builder2.AddAttribute(30, "RouterLink", (object)("/ca-nhan/dang-ky/dang-ky-nghi-ca-nhan"));
                __builder2.AddAttribute(31, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 15 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                           NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(32, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(33, "\r\n        Đăng ký nghỉ\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(34, "\r\n    ");
                __builder2.OpenComponent<global::AntDesign.MenuItem>(35);
                __builder2.AddAttribute(36, "Key", (object)("/ca-nhan/dang-ky/dang-ky-lam-them-gio"));
                __builder2.AddAttribute(37, "RouterLink", (object)("/ca-nhan/dang-ky/dang-ky-lam-them-gio"));
                __builder2.AddAttribute(38, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 19 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                           NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(39, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(40, "\r\n        Đăng ký làm thêm giờ\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(41, "\r\n    ");
                __builder2.OpenComponent<global::AntDesign.MenuItem>(42);
                __builder2.AddAttribute(43, "Key", (object)("/ca-nhan/dang-ky/giai-trinh-cham-cong"));
                __builder2.AddAttribute(44, "RouterLink", (object)("/ca-nhan/dang-ky/giai-trinh-cham-cong"));
                __builder2.AddAttribute(45, "RouterMatch", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 23 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffMenu.razor"
                           NavLinkMatch.All

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(46, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(47, "\r\n        Đăng ký giải trình chấm công\r\n    ");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
