#pragma checksum "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf69007a2835964682cd121a38d308971f1858f7"
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
    public partial class TextFieldWithMask<TValue> : InputBase<TValue>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 4 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
 if (isLabel)
{
    if (!string.IsNullOrWhiteSpace(Label))
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "label");
            __builder.AddAttribute(1, "class", "form-control-label");
            __builder.AddAttribute(2, "for", 
#nullable restore
#line 8 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                Id

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(3, 
#nullable restore
#line 9 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
             Label

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 10 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
             if (Required)
            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(4, "<font color=\"red\">(*)</font>");
#nullable restore
#line 13 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 15 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<DoAnTotNghiep.Components.LabelFor>(5);
            __builder.AddAttribute(6, "FieldIdentifier", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Forms.FieldIdentifier>(
#nullable restore
#line 18 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                   FieldIdentifier

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "HasRequired", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 18 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                                 Required

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 19 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
    }
}

#line default
#line hidden
#nullable disable
            __builder.OpenElement(8, "input");
            __builder.AddAttribute(9, "id", 
#nullable restore
#line 21 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
            Id

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(10, "class", "ant-input" + " " + (
#nullable restore
#line 21 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                        Class

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 21 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                               Mask

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "integer-digits", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                        IntergerDigits

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(12, "digits", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                 Digits

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(13, "max", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                               Max

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(14, "placeholder", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                                                  PlaceHolder

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(15, "disabled", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                                                                          Disable

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(16, "minus", 
#nullable restore
#line 22 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                                                                                                           Minus

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(17, "style", 
#nullable restore
#line 23 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
               Style

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(18, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 21 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                        CurrentValue

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => CurrentValue = __value, CurrentValue));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n\r\n\r\n");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "class", "form-control-validation");
            __builder.OpenComponent<DoAnTotNghiep.Components.CustomValidationMessage<string>>(23);
            __builder.AddAttribute(24, "Field", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Forms.FieldIdentifier>(
#nullable restore
#line 34 "C:\Users\Admin\Documents\VADoAnTotNghiep\DoAnTotNghiep\Components\TextFieldWithMask.razor"
                                    FieldIdentifier

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
