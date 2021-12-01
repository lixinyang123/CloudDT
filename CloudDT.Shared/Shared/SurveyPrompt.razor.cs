using Microsoft.AspNetCore.Components;

namespace CloudDT.Shared.Shared
{
    public class SurveyPromptBase : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }
    }
}
