#pragma checksum "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Timekeeping\SetupTimekeeping.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8785b406e26c0862d138b35c35df1d3dda9e7192"
// <auto-generated/>
#pragma warning disable 1591
namespace DoAnTotNghiep.Timekeeping
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/cham-cong/quy-dinh-cham-cong")]
    public partial class SetupTimekeeping : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<AntDesign.PageHeader>(0);
            __builder.AddAttribute(1, "Class", "site-page-header");
            __builder.AddAttribute(2, "Title", "Thiết lập quy định chấm công");
            __builder.AddAttribute(3, "PageHeaderExtra", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<AntDesign.Breadcrumb>(4);
                __builder2.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(6);
                    __builder3.AddAttribute(7, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<AntDesign.Icon>(8);
                        __builder4.AddAttribute(9, "Type", "home");
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(10, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(11);
                    __builder3.AddAttribute(12, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(13, "Chấm công");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(14, "\r\n            ");
                    __builder3.OpenComponent<AntDesign.BreadcrumbItem>(15);
                    __builder3.AddAttribute(16, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(17, "Thiết lập quy định chấm công");
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 11 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Timekeeping\SetupTimekeeping.razor"
 if (PermissionClaim.SETUP_TIMEKEEPING)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(18, "div");
            __builder.AddAttribute(19, "class", "site-content");
            __builder.OpenComponent<AntDesign.Tabs>(20);
            __builder.AddAttribute(21, "DefaultActiveKey", "0");
            __builder.AddAttribute(22, "ActiveKey", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 14 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Timekeeping\SetupTimekeeping.razor"
                                                   activeKey

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(23, "ActiveKeyChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => activeKey = __value, activeKey))));
            __builder.AddAttribute(24, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<AntDesign.TabPane>(25);
                __builder2.AddAttribute(26, "Tab", "Ca làm việc");
                __builder2.AddAttribute(27, "Key", "0");
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(28, "\r\n            ");
                __builder2.OpenComponent<AntDesign.TabPane>(29);
                __builder2.AddAttribute(30, "Tab", "Hệ số làm thêm giờ");
                __builder2.AddAttribute(31, "Key", "1");
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(32, "\r\n            ");
                __builder2.OpenComponent<AntDesign.TabPane>(33);
                __builder2.AddAttribute(34, "Tab", "Kiểu công");
                __builder2.AddAttribute(35, "Key", "2");
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(36, "\r\n            ");
                __builder2.OpenComponent<AntDesign.TabPane>(37);
                __builder2.AddAttribute(38, "Tab", "Công thức chấm công");
                __builder2.AddAttribute(39, "Key", "3");
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
#nullable restore
#line 29 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Timekeeping\SetupTimekeeping.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
