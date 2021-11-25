using Microsoft.AspNetCore.Components;

namespace CloudDT.Shared.Pages
{
    public partial class IndexBase : ComponentBase
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        [Inject] NavigationManager? NavigationManager { get; set; }

        public void Login()
        {
            NavigationManager?.NavigateTo("Overview");
        }
    }
}