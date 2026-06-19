//This area is used for using directive.
using Microsoft.AspNetCore.WebUtilities;
using NovelsSearchApp.Components.Pages;
using NovelsSearchApp.NovelModels;
using Microsoft.AspNetCore.Components;

namespace NovelsSearchApp.Components.Pages.UserInput;

public partial class CriteriaOfSearchingNovels
{
    public string? HighPriorityCriteria { get; set; }
    public string? LowPriorityCriteria { get; set; }
    public string? HighPriorityExcept { get; set; }
    public string? LowPriorityExcept {get; set; }

    /*public string SettingCritera()
    {
        
    }*/
        
    
}