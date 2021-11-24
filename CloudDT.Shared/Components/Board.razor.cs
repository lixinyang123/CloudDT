using Blazored.LocalStorage;
using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace CloudDT.Components
{
    public class Borad : ComponentBase
    {
        [Inject]
        ILocalStorageService? LocalStorage { get; set; }

        public List<CodeSnippet> CodeSnippets { get; set; } = new();

        public Selection<CodeSnippet> Selection { get; set; } = new();

        public List<IDetailsRowColumn<CodeSnippet>> Columns { get; set; } = new();

        protected override void OnInitialized()
        {
            Selection.GetKey = (item => item.Id);
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Key", x => x.Id!) { MaxWidth = 250, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Language", x => x.Language!) { MaxWidth = 100, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Name", x => x.Name!) { MaxWidth = 200, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Description", x => x.Description!) { IsResizable = true });
            base.OnInitialized();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            (await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets")).ForEach(i => CodeSnippets.Add(i));
        }
    }
}