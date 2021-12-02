using CloudDT.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CloudDT.Shared.Pages
{
    public partial class ShellBase : ComponentBase 
    {
        [Inject]
        public ContainerService? ContainerService { get; set; }
    }
}