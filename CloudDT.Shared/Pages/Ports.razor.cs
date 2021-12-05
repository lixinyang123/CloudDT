using BlazorFluentUI;
using CloudDT.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDT.Shared.Pages
{
    public class PortsBase : ComponentBase
    {
        [Inject]
        public ContainerService? ContainerService { get; set; }

        public Selection<KeyValuePair<int, string>> Selection { get; set; } = new();

        public List<IDetailsRowColumn<KeyValuePair<int, string>>> Columns { get; set; } = new();

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

        public List<KeyValuePair<int, string>>? Ports { get => ContainerService?.Ports.ToList(); }

        public string Port { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            CommandBarItems = new()
            {
                new CommandBarItem()
                {
                    Text = "Open",
                    IconName = "globe",
                    Key = "2",
                    Command = new RelayCommand(_ => System.Console.Write("open in browser"))
                },
                new CommandBarItem()
                {
                    Text = "Add",
                    IconName = "add",
                    Key = "5",
                    Command = new RelayCommand(_ => ShowDialog = true)
                },
                new CommandBarItem()
                {
                    Text = "Delete",
                    IconName = "delete",
                    Key = "3",
                    Command = new RelayCommand(DeletePort)
                }
            };

            Selection.GetKey = (item => item.Key);
            Columns.Add(new DetailsRowColumn<KeyValuePair<int, string>>("Port", x => x.Key) { MaxWidth = 250, IsResizable = true });
            Columns.Add(new DetailsRowColumn<KeyValuePair<int, string>>("Href", x => x.Value) { MaxWidth = 100, IsResizable = true });

            await base.OnInitializedAsync();
        }

        public async void Save()
        {
            if (int.TryParse(Port, out int port))
            {
                if (!await ContainerService!.ForwardPort(port))
                    return;

                Port = string.Empty;
                ShowDialog = false;
            }
        }

        private void DeletePort(object? _)
        {
            Selection.SelectedItems.ToList().ForEach(i =>
            {
                if (i.Key == 80 || i.Key == 8435 || i.Key == 7681)
                    return;

                ContainerService?.Ports.Remove(i.Key);
            });

            StateHasChanged();
        }
    }
}
