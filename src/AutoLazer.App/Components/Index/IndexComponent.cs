using Microsoft.AspNetCore.Blazor.Components;
using System.Diagnostics;
namespace AutoLazer.App
{
    public class IndexComponent : BlazorComponent
    {
        public string Name = "Adam";

        public void InputAreaOnKeyUp()
        {
            Debug.WriteLine("It Worked");
        }
    }
}