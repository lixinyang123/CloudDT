using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Text.Json;
using System.Collections.Generic;
namespace CloudDT.Components
{
    public class Borad : ComponentBase
    {
        [Inject] ILocalStorageService? localStorage { get; set; }
        public List<DataItem> InputList = new();
        public List<IDetailsRowColumn<DataItem>> Columns = new();

        protected async void initBoradList(string key) {
            if(localStorage == null) {
                return;
            }
            string codeCache = await localStorage.GetItemAsync<string>(key);
            if(codeCache != null && !codeCache.Trim().Equals("")) {
                InputList = JsonSerializer.Deserialize<List<DataItem>>(codeCache);
            }
        }
        protected override  void OnInitialized()
        {
            initBoradList("codeCache");
            Columns.Add(new DetailsRowColumn<DataItem>("CodeLanguage", x => x.CodeLanguage!) { MaxWidth = 70, Index = 0 });
            Columns.Add(new DetailsRowColumn<DataItem>("CodeName", x => x.CodeName!) { Index = 1, MaxWidth = 150, IsResizable = true });
            Columns.Add(new DetailsRowColumn<DataItem>("Description", x => x.Description!) { Index = 2 });
            base.OnInitialized();
        }
    }
}