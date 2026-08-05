//This area is used for using directive.
using Microsoft.AspNetCore.WebUtilities;
using NovelsSearchApp.Components.Pages;
using NovelsSearchApp.Models;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;


namespace NovelsSearchApp.Components.Pages.UserInput;

public partial class SearchCriteria
{

    public SearchConditionModel NovelParameters {get; set; }= new SearchConditionModel();
    public required string QueryStringForGetAsync { get; set; }//Why "required" modifier is needed?=If this property has no contents, this application can't function.
    
    public string ChangeQueryString()
    {
        //This is Original URI of narouAPI for AddQueryString.
        string narouUri = "novelapi/api/"; //Maybe content of narouUri should be "https://api.syosetu.com/novelapi/api/"

        var queryParamOfHP = SearchConditionDict.AddToDictionary(NovelParameters);

        QueryStringForGetAsync = QueryHelpers.AddQueryString(narouUri, queryParamOfHP);
        return QueryStringForGetAsync;//This property is set in GetAsync in `NovelsSearch.razor.cs`.
    }

    public string CheckUserInput()
    {
        if (!string.IsNullOrWhiteSpace(NovelParameters.HighPriorityCriteria))
        {
            return NovelParameters.HighPriorityCriteria;
        }
        else
        {
            return "`input` tag can't accept user's control";
        }
    }
}