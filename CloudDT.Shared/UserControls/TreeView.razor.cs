using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorFluentUI.Routing;
using Microsoft.AspNetCore.Components;

namespace CloudDT.UserControls
{
    public partial class TreeView : ComponentBase
    {
        public List<NavBarItem>? items;

        protected override Task OnInitializedAsync()
        {
            items = new List<NavBarItem> {
                new NavBarItem() {Text= "First", Url="#test1", NavMatchType= NavMatchType.AnchorOnly, Id="test3", IconName="Remove", Key="3"},
                new NavBarItem() {Text= "Second", Url="#test2", NavMatchType= NavMatchType.AnchorOnly, Id="test3", IconName="Remove", Key="3"},
                new NavBarItem() {Text= "Third", Url="#test3", NavMatchType= NavMatchType.AnchorOnly, Id="test3", IconName="Remove", Key="3"},
                new NavBarItem() {Text= "Fourth", Url="#test4", NavMatchType= NavMatchType.AnchorOnly, Id="test4", IconName="Save", Key="4"}
            };

            return base.OnInitializedAsync();
        }
    }
}