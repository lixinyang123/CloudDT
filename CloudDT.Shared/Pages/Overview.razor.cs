using Microsoft.AspNetCore.Components;

namespace CloudDT.Pages
{
    public class Overview : ComponentBase 
    {
        public string? SelectedKey { get; set; }

        protected override void OnInitialized()
        {
            SelectedKey = "Board";
            base.OnInitialized();
        }
    }
}