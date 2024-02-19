using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public static class JSFunction
    {
        public static async Task<(int, string)> InsertTextToTextareaAsync(this IJSRuntime jSRuntime, string id, string text)
        {
            var data = await jSRuntime.InvokeAsync<System.Text.Json.JsonElement>("insertTextToTextarea", id, text);
            return (data.GetProperty("cursorPosition").GetInt32(), data.GetProperty("text").GetString());
        }

        public static void SetCursorPosition(this IJSRuntime jSRuntime, int position)
        {
            jSRuntime.InvokeVoidAsync("setCursorPosition", position);
        }

        public static void SaveAsFile(this IJSRuntime jSRuntime, string name, string base64)
        {
            jSRuntime.InvokeVoidAsync("saveAsFile", name, base64);
        }

        public static void DownloadFileFromUrl(this IJSRuntime jSRuntime, string url, string name)
        {
            jSRuntime.InvokeVoidAsync("downloadFile", url, url, name, HttpMethod.Get.ToString());
        }

        public static async Task SaveAsFileAsync(this IJSRuntime jSRuntime, string name, string base64)
        {
            await jSRuntime.InvokeVoidAsync("saveAsFile", name, base64);
        }

        public static async Task DownloadFileFromUrl(this IJSRuntime jSRuntime,
            string url, string method, object filter, string fileName,
            DotNetObjectReference<JSCallback> dotNetObjectReference, string methodName)
        {
            await jSRuntime.InvokeVoidAsync("downloadFile", url, method, filter, fileName, dotNetObjectReference, methodName);
        }

        public static async Task Login(this IJSRuntime jSRuntime,
            string url, string userName, string password, bool isRememberMe,
            DotNetObjectReference<JSCallback> dotNetObjectReference, string methodName)
        {
            await jSRuntime.InvokeVoidAsync("login", url, userName, password, isRememberMe, dotNetObjectReference, methodName);
        }

        public static async Task OpenNewTabAsync(this IJSRuntime jSRuntime, string url)
        {
            await jSRuntime.InvokeVoidAsync("openNewTab", url);
        }

        public static async Task AddCookies(this IJSRuntime jSRuntime, string name, string value)
        {
            await jSRuntime.InvokeVoidAsync("addCookies", name, value);
        }

        public static async Task DownloadFileSimple(this IJSRuntime jSRuntime, string filePathUrl, string path, string AttachFileName, string method)
        {
            await jSRuntime.InvokeVoidAsync("downloadFileSimple", filePathUrl, path, AttachFileName, method);
        }
    }
}
