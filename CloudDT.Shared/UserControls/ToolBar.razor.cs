using BlazorFluentUI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudDT.UserControls
{
    public partial class ToolBar : ComponentBase
    {
        [Inject] IJSRuntime? JSRuntime { get; set; }

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

        public List<CommandBarItem> CommandBarItems { get; set; }

        public Dictionary<string, string> DropdownItems { get; set; }

        private string? currentLanguage;

        public string CurrentLanguage
        {
            get => currentLanguage ?? "Language";
            set
            {
                currentLanguage = value;

                JSRuntime?.InvokeVoidAsync(
                    "initEditor",
                    DropdownItems.SingleOrDefault(i => i.Key == value).Value
                ).AsTask();

                StateHasChanged();
            }
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public ToolBar()
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
        }

        public void Run(object? args)
        {
            ShowOffcanvas = true;
        }

        public void Stop(object? args)
        {
            ShowOffcanvas = false;
        }

        public void Save()
        {
            //CodeSnippetService?.AddSnippet(new()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = Name,
            //    Language = CurrentLanguage,
            //    Description = Description
            //});
        }

        private void OpenFind(object? args) => JSRuntime?.InvokeVoidAsync("openFind").AsTask().Wait();

        public string GetCode() => ((IJSInProcessRuntime)JSRuntime!).Invoke<string>("getCode");
    }
}