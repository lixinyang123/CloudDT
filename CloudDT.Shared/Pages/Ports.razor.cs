using BlazorFluentUI;
using CloudDT.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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

        protected override async Task OnInitializedAsync()
        {
            CommandBarItems = new()
            {
                new CommandBarItem()
                {
                    Text = "Refresh",
                    IconName = "arrow_sync_circle",
                    Key = "1"
                },
                new CommandBarItem()
                {
                    Text = "Open",
                    IconName = "code",
                    Key = "2"
                },
                new CommandBarItem()
                {
                    Text = "Delete",
                    IconName = "delete",
                    Key = "3"
                },
                new CommandBarItem()
                {
                    Text = "Info",
                    IconName = "error_circle",
                    Key = "5"
                }
            };

            Selection.GetKey = (item => item.Key);
            Columns.Add(new DetailsRowColumn<KeyValuePair<int, string>>("Port", x => x.Key) { MaxWidth = 250, IsResizable = true });
            Columns.Add(new DetailsRowColumn<KeyValuePair<int, string>>("Href", x => x.Value) { MaxWidth = 100, IsResizable = true });

            await base.OnInitializedAsync();
        }
    }
}
