using Microsoft.AspNetCore.Components;

namespace CodeMaker.Pages
{
    public class Overview : ComponentBase 
    {
        public string? SelectedKey { get; set; }

        protected override void OnInitialized()
        {
            SelectedKey = "Borad";
            base.OnInitialized();
        }
    }
}