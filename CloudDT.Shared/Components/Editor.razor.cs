using Microsoft.AspNetCore.Components;
using BlazorFluentUI;
using CloudDT.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace CloudDT.Components
{
    public class Editor : ComponentBase { 
        public bool largeDialogOpen = true;
        public string? uncontrolledSingleSelectionResult;
        public bool isBlocking = true;
        public List<DataItem>? items;

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public void UncontrolledSingleChangeHandler(DropdownChangeArgs args)
        {
            uncontrolledSingleSelectionResult = args.Option.Key;
        }
    }
}