using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorFluentUI;
using Microsoft.AspNetCore.Components;

namespace CloudDT.UserControls
{
    public partial class ToolBar : ComponentBase
    {
        private System.Windows.Input.ICommand? buttonCommand;

        public List<CommandBarItem>? items;

        protected override Task OnInitializedAsync()
        {
            buttonCommand = new RelayCommand((p) =>
            {
                StateHasChanged();
            });

            items = new List<CommandBarItem> 
            {
                new CommandBarItem() {Text= "Run", IconName="play", Key="1", Command=buttonCommand, CommandParameter="Run"},
                new CommandBarItem() {Text= "Stop", IconName="checkbox_unchecked", Key="2", Command=buttonCommand, CommandParameter="Stop"},
                new CommandBarItem() {Text= "Save", IconName="save", Key="4", Command=buttonCommand, CommandParameter="Save"},
                new CommandBarItem() {Text= "Search", IconName="search", Key="3", Command=buttonCommand, CommandParameter="Search"},
                new CommandBarItem() {Text= "Share", IconName="share", Key="5", Command=buttonCommand, CommandParameter="Share"}
            };

            return Task.CompletedTask;
        }
    }
}