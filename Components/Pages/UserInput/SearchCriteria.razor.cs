//This area is used for using directive.
using Microsoft.AspNetCore.WebUtilities;
using NovelsSearchApp.Components.Pages;
using NovelsSearchApp.Models;
using Microsoft.AspNetCore.Components;


namespace NovelsSearchApp.Components.Pages.UserInput;

public partial class SearchCriteria
{

    public SearchConditionModel NovelParameters = new SearchConditionModel();
    public required string QueryStringForGetAsync { get; set; } = default!;//Why "required" modifier is needed?
    
    public string ChangeQueryString()
    {
        //This is Original URI of narouAPI for AddQueryString.
        string narouUri = "novelapi/api/"; //Maybe content of narouUri should be "https://api.syosetu.com/novelapi/api/"

        var queryParamOfHP = SearchConditionDict.AddToDictionary(NovelParameters);
        //var queryParamOfHE = SearchConditionDict.AddToDictionary(HighPriorityExcept);

        QueryStringForGetAsync = QueryHelpers.AddQueryString(narouUri, queryParamOfHP);
        return QueryStringForGetAsync;//This property is set in GetAsync in `NovelsSearch.razor.cs`.
    }
    
}