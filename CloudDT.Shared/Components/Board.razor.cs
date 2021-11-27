using Blazored.LocalStorage;
using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDT.Shared.Components
{
    public partial class BoradBase : ComponentBase
    {
        [Inject]
        ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        NavigationManager? NavigationManager { get; set; }

        public Selection<CodeSnippet> Selection { get; set; } = new();

        public List<IDetailsRowColumn<CodeSnippet>> Columns { get; set; } = new();

        public List<CommandBarItem>? CommandBarItems { get; set; }

        private bool showDialog = false;

        public bool ShowDialog
        {
            get => showDialog;
            set
            {
                showDialog = value;
                StateHasChanged();
            }
        }

        public string ModalShow { get => showDialog ? "show" : string.Empty; }

        public string ModalDisplay { get => showDialog ? "block" : "none"; }

        public List<CodeSnippet> CodeSnippets { get; set; } = new();

        public CodeSnippet CurrentSnippet { get => Selection.GetSelection().FirstOrDefault() ?? new CodeSnippet(); }

        public string Keyword { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            CommandBarItems = new List<CommandBarItem>()
            {
                new CommandBarItem()
                {
                    Text= "Refresh",
                    IconName="arrow_sync_circle",
                    Key="1",
                    Command = new RelayCommand(Refresh)
                },
                new CommandBarItem()
                {
                    Text= "Open",
                    IconName="code",
                    Key="2",
                    Command = new RelayCommand(Open)
                },
                new CommandBarItem()
                {
                    Text= "Delete",
                    IconName="delete",
                    Key="3",
                    Command = new RelayCommand(Delete)
                },
                new CommandBarItem()
                {
                    Text= "Info",
                    IconName="error_circle",
                    Key="5",
                    Command = new RelayCommand(ShowInfo)
                }
            };

            Selection.GetKey = (item => item.Id);
            Columns.Add(new DetailsRowColumn<CodeSnippet>("ID", x => x.Id!) { MaxWidth = 250, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Language", x => x.Language!) { MaxWidth = 100, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Name", x => x.Name!) { MaxWidth = 200, IsResizable = true });
            Columns.Add(new DetailsRowColumn<CodeSnippet>("Description", x => x.Description!) { IsResizable = true });

            (await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets")).ForEach(i => CodeSnippets.Add(i));
            CodeSnippets.Reverse();

            await base.OnInitializedAsync();
        }

        private async void Refresh(object? _)
        {
            CodeSnippets.Clear();
            (await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets")).ForEach(i => CodeSnippets.Add(i));
            StateHasChanged();
        }

        private void Open(object? _)
        {
            NavigationManager?.NavigateTo($"/Overview#{CurrentSnippet.Id}", false);
        }

        private async void Delete(object? _)
        {
            foreach (CodeSnippet snippet in Selection.GetSelection())
            {
                await LocalStorage!.RemoveItemAsync($"Snippet-{snippet.Id}");
                CodeSnippets.Remove(snippet);
            }
            await LocalStorage!.SetItemAsync("CodeSnippets", CodeSnippets);
            Selection.ClearSelection();
            StateHasChanged();
        }

        private void ShowInfo(object? _)
        {
            if (string.IsNullOrEmpty(CurrentSnippet.Id))
                return;

            StateHasChanged();
            ShowDialog = true;
        }

        public async void Search(object? _)
        {
            if (string.IsNullOrEmpty(Keyword))
                return;

            CodeSnippets.Clear();

            (await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets")).ForEach(i =>
            {
                if (i.Name!.Contains(Keyword) || i.Description!.Contains(Keyword))
                    CodeSnippets.Add(i);
            });

            StateHasChanged();
        }

        public async void Save()
        {
            CodeSnippet snippet = CodeSnippets.Single(i => i.Id == CurrentSnippet.Id);
            snippet.Name = CurrentSnippet.Name;
            snippet.Description = CurrentSnippet.Description;

            await LocalStorage!.SetItemAsync("CodeSnippets", CodeSnippets);
            StateHasChanged();
            ShowDialog = false;
        }
    }
}