using Blazored.LocalStorage;
using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDT.Shared.UserControls
{
    public partial class ToolBarBase : ComponentBase
    {
        [Inject]
        ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        IJSRuntime? JSRuntime { get; set; }

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

        private bool showOffcanvas = false;

        public bool ShowOffcanvas
        {
            get => showOffcanvas;
            set
            {
                showOffcanvas = value;
                StateHasChanged();
            }
        }

        public string ModalShow { get => showDialog ? "show" : string.Empty; }

        public string ModalDisplay { get => showDialog ? "block" : "none"; }

        public string OffcanvasShow { get => showOffcanvas ? "show" : string.Empty; }

        public string OffcanvasVisibility { get => showOffcanvas ? "visible" : "hidden"; }

        public List<CommandBarItem>? CommandBarItems { get; set; }

        public Dictionary<string, string>? DropdownItems { get; set; }

        public List<CodeSnippet>? CodeSnippets { get; set; }

        private string? currentLanguage;

        public string CurrentLanguage
        {
            get => currentLanguage ?? "Language";
            set
            {
                currentLanguage = value;

                JSRuntime?.InvokeVoidAsync(
                    "initEditor",
                    DropdownItems?.SingleOrDefault(i => i.Key == value).Value
                ).AsTask();

                StateHasChanged();
            }
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CommandBarItems = new List<CommandBarItem>()
            {
                new CommandBarItem()
                {
                    Text= "Run",
                    IconName="play",
                    Key="1",
                    Command=new RelayCommand(Run)
                },
                new CommandBarItem()
                {
                    Text= "Stop",
                    IconName="checkbox_unchecked",
                    Key="2",
                    Command=new RelayCommand(Stop)
                },
                new CommandBarItem()
                {
                    Text= "Save",
                    IconName="save",
                    Key="4",
                    Command=new RelayCommand(args => ShowDialog = true)
                },
                new CommandBarItem()
                {
                    Text= "Search",
                    IconName="search",
                    Key="3",
                    Command=new RelayCommand(OpenFind)
                },
                new CommandBarItem()
                {
                    Text= "Share",
                    IconName="share",
                    Key="5"
                }
            };

            DropdownItems = new Dictionary<string, string>
            {
                { "Node", "javascript" },
                { "Python", "python" },
                { "CSharp", "csharp" }
            };

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            CodeSnippets = await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets") ?? new List<CodeSnippet>();
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Run(object? args)
        {
            ShowOffcanvas = true;
        }

        public void Stop(object? args)
        {
            ShowOffcanvas = false;
        }

        public async Task Save()
        {
            CodeSnippet snippet = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Language = currentLanguage ?? "PlainText",
                Description = Description
            };

            CodeSnippets?.Add(snippet);

            await LocalStorage!.SetItemAsync("CodeSnippets", CodeSnippets);
            await LocalStorage!.SetItemAsync($"Snippet-{snippet.Id}", GetCode());

            ShowDialog = false;
        }

        private void OpenFind(object? args) => JSRuntime?.InvokeVoidAsync("openFind").AsTask();

        public string GetCode() => ((IJSInProcessRuntime)JSRuntime!).Invoke<string>("getCode");
    }
}