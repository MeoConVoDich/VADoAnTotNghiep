#pragma checksum "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27918cb338fefad44b21a5a02a8b7c7d8971553b"
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
#nullable restore
#line 15 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.Domain;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\_Imports.razor"
using DoAnTotNghiep.ViewModel;

#line default
#line hidden
#nullable disable
    public partial class StaffInfoMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 1 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
 if (PermissionClaim.IsBusinessProfileMenuVisible())
{

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<AntDesign.Menu>(0);
            __builder.AddAttribute(1, "Theme", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.MenuTheme>(
#nullable restore
#line 3 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                 MenuTheme.Dark

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Class", "main_menu");
            __builder.AddAttribute(3, "Mode", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.MenuMode>(
#nullable restore
#line 3 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                                                         MenuMode.Horizontal

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "DefaultSelectedKeys", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<System.String>>(
#nullable restore
#line 4 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                                new[] { "/thong-tin-nhan-su/ho-so-nhan-vien" }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "TriggerSubMenuAction", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<AntDesign.Trigger>(
#nullable restore
#line 4 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                                                                                                       Trigger.Hover

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 5 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
         if (PermissionClaim.STAFFPROFILE_VIEW)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(7);
                __builder2.AddAttribute(8, "Key", "/thong-tin-nhan-su/ho-so-nhan-vien");
                __builder2.AddAttribute(9, "RouterLink", "/thong-tin-nhan-su/ho-so-nhan-vien");
                __builder2.AddAttribute(10, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 7 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                                                                                                                            NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(11, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(12, "\r\n                Hồ sơ nhân viên\r\n            ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 10 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
         if (PermissionClaim.DISCIPLINE_BONUS_VIEW)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<AntDesign.MenuItem>(13);
                __builder2.AddAttribute(14, "Key", "/thong-tin-nhan-su/thong-tin-khen-thuong-ky-luat");
                __builder2.AddAttribute(15, "RouterLink", "/thong-tin-nhan-su/thong-tin-khen-thuong-ky-luat");
                __builder2.AddAttribute(16, "RouterMatch", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 13 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
                                                                                                                                                        NavLinkMatch.All

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(18, "\r\n                Thông tin khen thưởng, kỷ luật\r\n            ");
                }
                ));
                __builder2.CloseComponent();
#nullable restore
#line 16 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
        }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 18 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Shared\StaffInfoMenu.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
