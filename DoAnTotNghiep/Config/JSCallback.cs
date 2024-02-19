using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public class JSCallback
    {
        public Action<object> CallbackAction;
        [JSInvokable("Callback_JSCallback")]
        public void Callback(object value)
        {
            CallbackAction?.Invoke(value);
        }

        public string GetNameCallbackMethod() => "Callback_JSCallback";
    }
}
