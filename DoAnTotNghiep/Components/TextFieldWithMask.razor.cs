using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Components
{
    public partial class TextFieldWithMask<TValue> : InputBase<TValue>
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [Parameter] public string Label { get; set; }
        [Parameter] public bool Required { get; set; }
        [Parameter] public string Mask { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public ICollection<KeyValuePair<string, string>> Datasource { get; set; }
        [Parameter] public int IntergerDigits { get; set; }
        [Parameter] public int Digits { get; set; }
        [Parameter] public long Max { get; set; } = long.MaxValue;
        [Parameter] public bool IgnoreValidate { get; set; } = false;
        [Parameter] public bool isLabel { get; set; } = true;
        [Parameter] public bool Disable { get; set; } = false;
        [Parameter] public string PlaceHolder { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public bool Minus { get; set; }

        public DotNetObjectReference<TextFieldWithMask<TValue>> DotNetRef;

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (value == "null")
            {
                value = null;
            }
            CultureInfo culture1 = CultureInfo.CreateSpecificCulture("vi-VN");
            DateTime dateTime;

            try
            {
                if (typeof(TValue) == typeof(string))
                {
                    result = (TValue)(object)value;
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(DateTime?))
                {
                    if (System.DateTime.TryParseExact(value, "dd/MM/yyyy", culture1, DateTimeStyles.None, out dateTime))
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else if (System.DateTime.TryParseExact(value, "MM/yyyy", culture1, DateTimeStyles.None, out dateTime))
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else if (DateTime.TryParseExact(value, "yyyy", culture1, DateTimeStyles.None, out dateTime) && dateTime != new DateTime())
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else
                    {
                        throw new InvalidOperationException("Dữ liệu còn thiếu hoặc không hợp lệ.");
                    }
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(DateTime))
                {
                    if (System.DateTime.TryParseExact(value, "dd/MM/yyyy", culture1, DateTimeStyles.None, out dateTime))
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else if (System.DateTime.TryParseExact(value, "MM/yyyy", culture1, DateTimeStyles.None, out dateTime))
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else if (System.DateTime.TryParseExact(value, "yyyy", culture1, DateTimeStyles.None, out dateTime) && dateTime != new DateTime())
                    {
                        result = (TValue)(object)dateTime;
                    }
                    else
                    {
                        throw new InvalidOperationException("Dữ liệu còn thiếu hoặc không hợp lệ.");
                    }
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(decimal))
                {
                    decimal.TryParse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture1, out decimal parsedValue);
                    result = (TValue)(object)parsedValue;
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(decimal?))
                {
                    if (value == null)
                    {
                        result = (TValue)(object)null;
                    }
                    else
                    {
                        decimal.TryParse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture1, out decimal parsedValue);
                        result = (TValue)(object)parsedValue;
                    }
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(int?))
                {
                    validationErrorMessage = null;
                    if (value == null)
                    {
                        result = (TValue)(object)null;
                    }
                    else
                    {
                        int.TryParse(value.Replace(".", ""), NumberStyles.Integer, culture1, out int parsedValue);
                        result = (TValue)(object)parsedValue;
                    }
                    return true;
                }
                else if (typeof(TValue) == typeof(long?))
                {
                    if (value == null)
                    {
                        result = (TValue)(object)null;
                    }
                    else
                    {
                        long.TryParse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture1, out long parsedValue);
                        result = (TValue)(object)parsedValue;
                    }
                    validationErrorMessage = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                result = default;
                validationErrorMessage = $"Dữ liệu không hợp lệ.";
                return false;
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            DotNetRef = DotNetObjectReference.Create(this);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("fieldInputWithMask.init", Id, DotNetRef, "Change_fieldInputWithMask", Mask);
            }
        }

        [JSInvokable("Change_fieldInputWithMask")]
        public void Change(string value)
        {
            if (IgnoreValidate)
            {
                TryParseValueFromString(value, out TValue outValue, out string validationErrorMessage);
                ValueChanged.InvokeAsync(outValue);
            }
            else
            {
                CurrentValueAsString = value;
            }
        }
    }
}
