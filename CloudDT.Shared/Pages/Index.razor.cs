using Microsoft.AspNetCore.Components;

namespace CloudDT.Shared.Pages
{
    public partial class IndexBase : ComponentBase
    {
        [Inject]
        NavigationManager? NavigationManager { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public void Login()
        {
            NavigationManager?.NavigateTo("/Board");
        }
    }
}