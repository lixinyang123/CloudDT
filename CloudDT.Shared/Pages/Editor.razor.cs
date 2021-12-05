using Blazored.LocalStorage;
using BlazorFluentUI;
using BlazorFluentUI.Routing;
using CloudDT.Shared.Models;
using CloudDT.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDT.Shared.Pages
{
    public class EditorBase : ComponentBase
    {

        [Inject]
        ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        NavigationManager? NavigationManager { get; set; }

        [Inject]
        private ContainerService? ContainerService { get; set; }

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

        public Dictionary<string, string> DropdownItems { get; set; } = new Dictionary<string, string>
        {
            { "PlainText", string.Empty },
            { "Node", "javascript" },
            { "Python", "python" },
            { "CSharp", "csharp" }
        };

        public List<NavBarItem> NavBarItems { get; set; } = new();

        public List<CodeSnippet>? CodeSnippets { get; set; }

        private CodeSnippet currentSnippet = new()
        {
            Language = "PlainText"
        };

        public CodeSnippet CurrentSnippet
        {
            get => currentSnippet;
            set
            {
                currentSnippet = value;
                LoadCodeSnippt(currentSnippet.Id);
                StateHasChanged();
            }
        }

        public string ResultFrame { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            CommandBarItems = new List<CommandBarItem>()
            {
                new CommandBarItem()
                {
                    Text= "Run",
                    IconName="play",
                    Key="1",
                    Command = new RelayCommand(Run)
                },
                new CommandBarItem()
                {
                    Text= "Stop",
                    IconName="checkbox_unchecked",
                    Key="2",
                    Command = new RelayCommand(Stop)
                },
                new CommandBarItem()
                {
                    Text= "Save",
                    IconName="save",
                    Key="4",
                    Command = new RelayCommand(Save)
                },
                new CommandBarItem()
                {
                    Text= "Search",
                    IconName="search",
                    Key="3",
                    Command = new RelayCommand(OpenFind)
                },
                new CommandBarItem()
                {
                    Text= "Share",
                    IconName="share",
                    Key="5"
                }
            };

            CodeSnippets = await LocalStorage!.GetItemAsync<List<CodeSnippet>>("CodeSnippets") ?? new List<CodeSnippet>();
            CodeSnippets.Reverse();

            CodeSnippets.ForEach(i =>
            {
                NavBarItems.Add(new()
                {
                    Text = i.Name,
                    NavMatchType = NavMatchType.AnchorOnly,
                    Id = i.Id,
                    IconName = "code",
                    Key = i.Id,
                    Url = $"/Editor/#{i.Id}",
                    Command = new RelayCommand(_ => CurrentSnippet = i)
                });
            });

            var snipptId = NavigationManager!.Uri.Split("#").Last();
            CurrentSnippet = CodeSnippets?.FirstOrDefault(i => i.Id == snipptId) ?? CurrentSnippet;

            await base.OnInitializedAsync();
        }

        private void InitEditor(string? lang = "", string? code = "") => JSRuntime?.InvokeVoidAsync("initEditor", lang, code).AsTask();

        private async void LoadCodeSnippt(string? id)
        {
            string lang = DropdownItems[CurrentSnippet.Language!];
            string code = await LocalStorage!.GetItemAsync<string>($"Snippet-{id}");
            InitEditor(lang, code);
        }

        public void ChangeEditorLang(string lang)
        {
            CurrentSnippet.Language = lang;
            InitEditor(DropdownItems[lang], GetCode());
        }

        public async void Run(object? _)
        {
            if (CurrentSnippet.Language == "PlainText")
                return;

            await JSRuntime!.InvokeAsync<string>(
                "runCodeSnippets",
                ContainerService?.ContainerId,
                CurrentSnippet.Language,
                GetCode()
            ).AsTask();

            if (true)
            {
                ResultFrame = $"{ContainerService!.Ports[8435]}/";
                ShowOffcanvas = true;
            }
        }

        public void Stop(object? _)
        {
            ResultFrame = string.Empty;
            ShowOffcanvas = false;
        }

        private async void Save(object? _)
        {
            if (CurrentSnippet.Id is not null)
            {
                await SaveCurrentSnippets();
                return;
            }

            ShowDialog = true;
        }

        public async Task SaveSnippets()
        {
            if (string.IsNullOrEmpty(CurrentSnippet.Name) || string.IsNullOrEmpty(CurrentSnippet.Description))
                return;

            CodeSnippet snippet = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = CurrentSnippet.Name,
                Language = CurrentSnippet.Language,
                Description = CurrentSnippet.Description
            };

            CodeSnippets?.Add(snippet);

            NavBarItems.Insert(0, new()
            {
                Text = snippet.Name,
                NavMatchType = NavMatchType.AnchorOnly,
                Id = snippet.Id,
                IconName = "code",
                Key = snippet.Id,
                Url = $"/Editor/#{snippet.Id}",
                Command = new RelayCommand(_ => CurrentSnippet = snippet)
            });

            await LocalStorage!.SetItemAsync("CodeSnippets", CodeSnippets);
            await LocalStorage!.SetItemAsync($"Snippet-{snippet.Id}", GetCode());

            ShowDialog = false;

            StateHasChanged();
        }

        public async Task SaveCurrentSnippets()
        {
            await LocalStorage!.SetItemAsync("CodeSnippets", CodeSnippets);
            await LocalStorage!.SetItemAsync($"Snippet-{CurrentSnippet.Id}", GetCode());
        }

        private void OpenFind(object? _) => JSRuntime?.InvokeVoidAsync("openFind").AsTask();

        public string GetCode() => ((IJSInProcessRuntime)JSRuntime!).Invoke<string>("getCode");

        public void NewSnippet()
        {
            NavigationManager?.NavigateTo("/Editor");
            CurrentSnippet = new() { Language = "PlainText" };
            LoadCodeSnippt(CurrentSnippet.Id);
        }
    }
}