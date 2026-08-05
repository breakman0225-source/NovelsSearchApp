//This area is used for using directive.
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using NovelsSearchApp.Models;

namespace NovelsSearchApp.Components.Pages.UserInput;

public class SearchConditionDict
{
    public static Dictionary<string, string?> AddToDictionary(SearchConditionModel parameters)
    {
        var keywordOfSearch = new Dictionary<string, string?>
        {
            {"out", "json"},
            {"of", "t-n-w-s-g-k-gp-nu"}
            //This "of" parameter specify output items. in this case, 
            // "title, wirter, abstract, genre, keyword, global_point, novelupdated_at, and ncode which is " is oututed.
        };

        if (!string.IsNullOrWhiteSpace(parameters.HighPriorityCriteria))
        {
            keywordOfSearch.Add("word", parameters.HighPriorityCriteria);
        }

        if (!string.IsNullOrWhiteSpace(parameters.Exceptword))
        {
            keywordOfSearch.Add("notword", parameters.Exceptword);
        }

        Debug.WriteLine($"{parameters.HighPriorityCriteria}");
        
        return keywordOfSearch;
    }

}