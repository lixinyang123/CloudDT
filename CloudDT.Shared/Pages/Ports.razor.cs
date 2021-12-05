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
                    Command = new RelayCommand(_ =>
                    {
                        Port = string.Empty;
                        if (int.TryParse(Port, out int port))
                            ContainerService?.ForwardPort(port);
                    })
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

        private void DeletePort(object? _)
        {
            Selection.SelectedItems.ToList().ForEach(i =>
            {
                if (i.Key == 80 || i.Key == 8435 || i.Key == 7681)
                    return;

                ContainerService?.Ports.Remove(i.Key);
            });
        }
    }
}
