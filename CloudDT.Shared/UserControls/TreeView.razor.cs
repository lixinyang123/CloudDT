using Blazored.LocalStorage;
using BlazorFluentUI.Routing;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDT.UserControls
{
    public partial class TreeView : ComponentBase
    {
        [Inject]
        ILocalStorageService? LocalStorage { get; set; }

        public List<NavBarItem> CodeSnippets { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            (await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets")).ForEach(i =>
            {
                CodeSnippets.Add(new()
                {
                    Text = i.Name,
                    Url = "#",
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = i.Id,
                    IconName = "Remove",
                    Key = "3"
                });
            });

            await base.OnInitializedAsync();
        }
    }
}