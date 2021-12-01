using BlazorFluentUI;
using BlazorFluentUI.Routing;
using BlazorFluentUI.Themes.Default;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDT.Shared.Shared
{
    public class OverviewLayoutBase : LayoutComponentBase
    {
        [Inject]
        private ThemeProvider? ThemeProvider { get; set; }

        public List<NavBarItem>? NavBarItems { get; set; }

        protected override Task OnInitializedAsync()
        {
            ThemeProvider?.UpdateTheme(new DefaultPaletteDark());

            NavBarItems = new()
            {
                new()
                {
                    Text = "Board",
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = "Board",
                    Key = "Board",
                    Url = "/Board"
                },
                new()
                {
                    Text = "Editor",
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = "Editor",
                    Key = "Editor",
                    Url = "/Editor"
                },
                new()
                {
                    Text = "Console",
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = "Console",
                    Key = "Console",
                    Url = "/Console"
                },
                new()
                {
                    Text = "CloudShell",
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = "CloudShell",
                    Key = "CloudShell",
                    Url = "/CloudShell"
                }
            };

            return base.OnInitializedAsync();
        }
    }
}