using Blazored.LocalStorage;
using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudDT.Components
{
    public class Borad : ComponentBase
    {
        [Inject] ILocalStorageService? LocalStorage { get; set; }

        public List<CodeSnippet>? InputList { get; set; }

        public List<IDetailsRowColumn<CodeSnippet>>? Columns { get; set; }

        protected async void InitBoradList(string key)
        {
            string codeCache = await LocalStorage!.GetItemAsync<string>(key);

            if (!string.IsNullOrEmpty(codeCache))
            {
                InputList = JsonSerializer.Deserialize<List<CodeSnippet>>(codeCache);
            }
        }

        protected override void OnInitialized()
        {
            InitBoradList("codeCache");
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("CodeLanguage", x => x.Language!) { MaxWidth = 70, Index = 0 });
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("CodeName", x => x.Name!) { Index = 1, MaxWidth = 150, IsResizable = true });
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("Description", x => x.Description!) { Index = 2 });
            base.OnInitialized();
        }
    }
}