using BlazorFluentUI;
using BlazorFluentUI.Themes.Default;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CloudDT.Shared.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        private ThemeProvider? ThemeProvider { get; set; }

        protected override Task OnInitializedAsync()
        {
            ThemeProvider?.UpdateTheme(new DefaultPaletteDark());
            return base.OnInitializedAsync();
        }
    }
}
