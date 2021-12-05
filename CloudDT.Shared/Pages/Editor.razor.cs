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

        public Dictionary<string, string>? DropdownItems { get; set; }

        public List<NavBarItem> NavBarItems { get; set; } = new();

        public List<CodeSnippet>? CodeSnippets { get; set; }

        private string? currentLanguage;

        public string CurrentLanguage
        {
            get => currentLanguage ?? "PlainText";
            set
            {
                currentLanguage = value;
                InitEditor(DropdownItems?.SingleOrDefault(i => i.Key == value).Value);

                StateHasChanged();
            }
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        private CodeSnippet? currentSnippet;

        public CodeSnippet? CurrentSnippet
        {
            get => currentSnippet;
            set
            {
                currentSnippet = value;
                CurrentLanguage = currentSnippet!.Language!;
                LoadCodeSnippt(currentSnippet?.Id!);
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
                    Command = new RelayCommand(_ => ShowDialog = true)
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

            DropdownItems = new Dictionary<string, string>
            {
                { "Node", "javascript" },
                { "Python", "python" },
                { "CSharp", "csharp" }
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
            LoadCodeSnippt(snipptId);

            await base.OnInitializedAsync();
        }

        private void InitEditor(string? lang = "", string? code = "") => JSRuntime?.InvokeVoidAsync("initEditor", lang, code).AsTask();

        private async void LoadCodeSnippt(string id)
        {
            string lang = CodeSnippets?.FirstOrDefault(i => i.Id == id)?.Language ?? string.Empty;
            string code = await LocalStorage!.GetItemAsync<string>($"Snippet-{id}");
            InitEditor(lang, code);
        }

        public async void Run(object? _)
        {
            if (CurrentLanguage == "PlainText")
                return;

            await JSRuntime!.InvokeAsync<string>(
                "runCodeSnippets",
                ContainerService?.ContainerId,
                CurrentLanguage, GetCode()
            ).AsTask();

            if (true)
            {
                ResultFrame = $"{ContainerService!.Ports[8435]}/";
                ShowOffcanvas = true;
            }
        }

        public void Stop(object? _)
        {
            ResultFrame = "";
            ShowOffcanvas = false;
        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
                return;

            CodeSnippet snippet = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Language = currentLanguage ?? "PlainText",
                Description = Description
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

        private void OpenFind(object? _) => JSRuntime?.InvokeVoidAsync("openFind").AsTask();

        public string GetCode() => ((IJSInProcessRuntime)JSRuntime!).Invoke<string>("getCode");
    }
}