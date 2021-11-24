using BlazorFluentUI;
using CloudDT.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace CloudDT.Components
{
    public class Borad : ComponentBase
    {
        public List<CodeSnippet>? CodeSnippets { get; set; }

        public List<IDetailsRowColumn<CodeSnippet>>? Columns { get; set; }

        protected override void OnInitialized()
        {
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("Language", x => x.Language!) { MaxWidth = 70, Index = 0 });
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("Name", x => x.Name!) { Index = 1, MaxWidth = 150, IsResizable = true });
            Columns?.Add(new DetailsRowColumn<CodeSnippet>("Description", x => x.Description!) { Index = 2 });
            base.OnInitialized();
        }
    }
}