using Microsoft.AspNetCore.Components;

namespace CloudDT.Pages
{
    public class Index : ComponentBase 
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        [Inject] NavigationManager? navigationManager { get; set; }

        public void Login()
        {
            navigationManager?.NavigateTo("Overview");
        }
    }
}