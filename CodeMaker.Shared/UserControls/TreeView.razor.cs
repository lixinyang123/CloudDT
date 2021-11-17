using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorFluentUI.Routing;
using Microsoft.AspNetCore.Components;

namespace CodeMaker.UserControls
{
    public partial class TreeView : ComponentBase
    {
        public List<NavBarItem>? items;

        protected override Task OnInitializedAsync()
        {
            items = new List<NavBarItem> {
                new NavBarItem() {
                    Text= "First",
                    Url="navBarPage#test1",
                    NavMatchType= NavMatchType.AnchorOnly,
                    Id="test1",
                    IconName="Home",
                    Key="1"},
                new NavBarItem() {
                    Text= "Second",
                    Id="test2",
                    IconName="Add",
                    Key="2",
                    IsExpanded=true,
                    Items = new List<NavBarItem> {
                        new NavBarItem
                        {
                            Text="SubFirst",
                            Id="subtest1",
                            IconName="Save",
                            Key="5",
                            IsExpanded=false,
                            Items = new List<NavBarItem>
                        {
                                new NavBarItem
                                {
                                    Text="SubSubFirst",
                                    Url="navBarPage#subsubtest1",
                                    NavMatchType=NavMatchType.AnchorOnly,
                                    Id="subsubtest1",
                                    IconName="Home",
                                    Key="7"
                                },
                                new NavBarItem
                                {
                                    Text="SubSubSecond",
                                    Url="navBarPage#subsubtest2",
                                    NavMatchType=NavMatchType.AnchorOnly,
                                    Id="subsubtest2",
                                    IconName="Add",
                                    Key="8"
                                }
                            }
                        },
                        new NavBarItem
                        {
                            Text="SubSecond",
                            Url="navBarPage#subtest2",
                            NavMatchType=NavMatchType.AnchorOnly,
                            Id="subtest2",
                            IconName="Save",
                            Key="6"
                        }
                    }
                },
                new NavBarItem() {Text= "Third", Url="navBarPage#test3", NavMatchType= NavMatchType.AnchorOnly, Id="test3", IconName="Remove", Key="3"},
                new NavBarItem() {Text= "Fourth", Url="navBarPage#test4", NavMatchType= NavMatchType.AnchorOnly, Id="test4", IconName="Save", Key="4"}
            };

            return base.OnInitializedAsync();
        }
    }
}