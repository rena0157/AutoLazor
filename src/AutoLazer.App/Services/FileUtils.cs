using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace AutoLazer.App
{
    public static class FileUtils
    {
        public static Task SaveFile(string filename, byte[] data) =>
            JSRuntime.Current.InvokeAsync<object>(
                "saveFile", filename, Convert.ToBase64String(data));
    }
}