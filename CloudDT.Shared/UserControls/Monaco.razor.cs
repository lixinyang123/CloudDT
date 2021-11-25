using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CloudDT.Shared.UserControls
{
    public partial class MonacoBase : ComponentBase
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            JSRuntime?.InvokeVoidAsync("initEditor").AsTask();
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}