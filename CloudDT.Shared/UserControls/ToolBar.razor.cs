using BlazorFluentUI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;

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

        public List<string> DropdownItems { get; set; }

        private string? currentLanguage;

        public string CurrentLanguage
        {
            get => currentLanguage ?? "Language";
            set
            {
                currentLanguage = value;
                StateHasChanged();
            }
        }

        public ToolBar()
        {
            CommandBarItems = new List<CommandBarItem>()
            {
                new CommandBarItem() {Text= "Run", IconName="play", Key="1", Command=new RelayCommand(Run) },
                new CommandBarItem() {Text= "Stop", IconName="checkbox_unchecked", Key="2", Command=new RelayCommand(Stop) },
                new CommandBarItem() {Text= "Save", IconName="save", Key="4", Command=new RelayCommand(Save) },
                new CommandBarItem() {Text= "Search", IconName="search", Key="3", Command=new RelayCommand(OpenFind) },
                new CommandBarItem() {Text= "Share", IconName="share", Key="5" }
            };

            DropdownItems = new List<string>()
            {
                "Node", "Python", "CSharp"
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

        public void Save(object? args)
        {
            string str = GetValue();
            ShowDialog = true;
        }

        private void OpenFind(object? args) => JSRuntime?.InvokeVoidAsync("openFind").AsTask();

        public string GetValue() => ((IJSInProcessRuntime)JSRuntime!).Invoke<string>("getCode");
    }
}