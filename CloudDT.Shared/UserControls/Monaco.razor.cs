using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CloudDT.UserControls
{
    public partial class Monaco : ComponentBase
    {
        [Inject] IJSRuntime? JSRuntime { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            JSRuntime?.InvokeVoidAsync("initEditor");
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}