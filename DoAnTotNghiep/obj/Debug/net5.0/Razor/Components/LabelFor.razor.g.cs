#pragma checksum "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "470b6ab2de6957fed2c23c967648200278b0a69b"
// <auto-generated/>
#pragma warning disable 1591
namespace DoAnTotNghiep.Components
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
    public partial class LabelFor : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 1 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
 if (Title != null)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "label");
            __builder.AddAttribute(1, "for", 
#nullable restore
#line 3 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
                 Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(2, 
#nullable restore
#line 4 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
         Title

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 5 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
         if (Required || HasRequired)
        {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(3, "<font color=\"red\">(*)</font>");
#nullable restore
#line 8 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 10 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(4, "label");
            __builder.AddAttribute(5, "for", 
#nullable restore
#line 13 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
                 Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(6, 
#nullable restore
#line 14 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
         Display

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 15 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
         if (Required || HasRequired)
        {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(7, "<font color=\"red\">(*)</font>");
#nullable restore
#line 18 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 20 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\LabelFor.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
