﻿using BlazorFluentUI;
using BlazorFluentUI.Routing;
using BlazorFluentUI.Themes.Default;
using CloudDT.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDT.Shared.Shared
{
    public class OverviewLayoutBase : LayoutComponentBase
    {
        [Inject]
        private ThemeProvider? ThemeProvider { get; set; }

        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        [Inject]
        private ContainerService? ContainerService { get; set; }

        public List<NavBarItem>? NavBarItems { get; set; }

        protected override async Task OnInitializedAsync()
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

            bool flag = await ContainerService!.Create();
            if (flag)
                await JSRuntime!.InvokeVoidAsync("dalayContainer", ContainerService?.ContainerId);

            await base.OnInitializedAsync();
        }
    }
}