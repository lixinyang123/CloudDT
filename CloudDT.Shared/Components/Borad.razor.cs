using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;

namespace CloudDT.Components
{
    public class Borad : ComponentBase
    {
        public System.Collections.Generic.List<DataItem> InputList = new();

        public Selection<DataItem> selection = new Selection<DataItem>();

        public System.Collections.Generic.List<IDetailsRowColumn<DataItem>> Columns = new();
        public System.Collections.Generic.List<IDetailsRowColumn<DataItem>> CustomColumns = new();

        protected override void OnInitialized()
        {
            selection.GetKey = (item => item.Key);
            Columns.Add(new DetailsRowColumn<DataItem>("Key", x => x.KeyNumber) { MaxWidth = 70, Index = 0 });
            Columns.Add(new DetailsRowColumn<DataItem>("Name", x => x.DisplayName!) { Index = 1, MaxWidth = 150, IsResizable = true });
            Columns.Add(new DetailsRowColumn<DataItem>("Description", x => x.Description!) { Index = 2 });

            for (var i = 0; i< 50; i++)
            {
                InputList.Add(new DataItem(i));
            }

            base.OnInitialized();
        }
    }
}