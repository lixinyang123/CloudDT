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

        public List<DataItem>? InputList { get; set; }

        public List<IDetailsRowColumn<DataItem>>? Columns { get; set; }

        protected async void InitBoradList(string key)
        {
            string codeCache = await LocalStorage!.GetItemAsync<string>(key);

            if (!string.IsNullOrEmpty(codeCache))
            {
                InputList = JsonSerializer.Deserialize<List<DataItem>>(codeCache);
            }
        }

        protected override void OnInitialized()
        {
            InitBoradList("codeCache");
            Columns?.Add(new DetailsRowColumn<DataItem>("CodeLanguage", x => x.CodeLanguage!) { MaxWidth = 70, Index = 0 });
            Columns?.Add(new DetailsRowColumn<DataItem>("CodeName", x => x.CodeName!) { Index = 1, MaxWidth = 150, IsResizable = true });
            Columns?.Add(new DetailsRowColumn<DataItem>("Description", x => x.Description!) { Index = 2 });
            base.OnInitialized();
        }
    }
}