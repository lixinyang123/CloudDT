using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CloudDT.UserControls
{
    public partial class Monaco : ComponentBase
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