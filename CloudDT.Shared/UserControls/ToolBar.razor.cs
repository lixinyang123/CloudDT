using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorFluentUI;
using Microsoft.AspNetCore.Components;

namespace CloudDT.UserControls
{
    public partial class ToolBar : ComponentBase
    {
        private System.Windows.Input.ICommand? runCommand;
        private System.Windows.Input.ICommand? saveCommand;
        private System.Windows.Input.ICommand? stopCommand; 
        private System.Windows.Input.ICommand? searchCommand;
        private System.Windows.Input.ICommand? shareCommand;
        public List<CommandBarItem>? items;

        protected override Task OnInitializedAsync()
        {
            runCommand = new RelayCommand((p) =>
            {
            });

            saveCommand = new RelayCommand((p) =>
            {
            });

            stopCommand = new RelayCommand((p) =>{
                
            });

            searchCommand = new RelayCommand((p) =>{
                
            });

            shareCommand = new RelayCommand((p) =>{
                
            });

            items = new List<CommandBarItem> 
            {
                new CommandBarItem() {Text= "Run", IconName="play", Key="1", Command=runCommand},
                new CommandBarItem() {Text= "Stop", IconName="checkbox_unchecked", Key="2", Command=stopCommand},
                new CommandBarItem() {Text= "Save", IconName="save", Key="4", Command=saveCommand},
                new CommandBarItem() {Text= "Search", IconName="search", Key="3", Command=searchCommand},
                new CommandBarItem() {Text= "Share", IconName="share", Key="5", Command=shareCommand}
            };
            return Task.CompletedTask;
        }
    }
}