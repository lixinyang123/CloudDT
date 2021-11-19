using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CloudDT.UserControls
{
    public partial class Monaco : ComponentBase
    {
        [Inject] IJSRuntime? jsRuntime { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            jsRuntime?.InvokeVoidAsync("initEditor");
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}