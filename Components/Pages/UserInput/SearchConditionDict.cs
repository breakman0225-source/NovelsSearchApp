//This area is used for using directive.
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components;
using NovelsSearchApp.Models;

namespace NovelsSearchApp.Components.Pages.UserInput;



public class SearchConditionDict
{
    public Dictionary<string, string?> keywordOfSearch = new Dictionary<string, string?>
        {
            {"out", "json"},
            {"of", "t-n-w-s-g-k-gp-nu"}
            //This "of" parameter specify output items. in this case, 
            // "title, wirter, abstract, genre, keyword, global_point, novelupdated_at, and ncode which is " is oututed.
        };

    public static Dictionary<string, string?> AddToDictionary(SearchConditionModel parameters)
    {
       var dictionaryForAddingKeyword = new SearchConditionDict();

        //This is the handle which specifies output of novels. `word` is specifies element of novels to be generated. `notword` is specifies element os novels to be not generated.
        if(!string.IsNullOrWhiteSpace(parameters.HighPriorityCriteria))
        {
            dictionaryForAddingKeyword.keywordOfSearch.Add("word", parameters.HighPriorityCriteria);
        }

        if(!string.IsNullOrWhiteSpace(parameters.Exceptword))
        {
            dictionaryForAddingKeyword.keywordOfSearch.Add("notword", parameters.Exceptword);
        }

        if(!string.IsNullOrWhiteSpace(parameters.OrderSelect))
        {
            dictionaryForAddingKeyword.keywordOfSearch.Add("order", parameters.OrderSelect);
        }

        if(!string.IsNullOrWhiteSpace(parameters.LimSelect))
        {
            dictionaryForAddingKeyword.keywordOfSearch.Add("lim", parameters.LimSelect);
        }

        if(parameters.CurrentPageNumber != 1)
        {
            var stForPagenate = parameters.CurrentPageNumber * int.Parse(parameters.LimSelect) + 1;
            dictionaryForAddingKeyword.keywordOfSearch.Add("st", stForPagenate.ToString());
        }

        return dictionaryForAddingKeyword.keywordOfSearch;
    }

    /*public static Dictionary<string, string?> OnSortOrderChanged()
    {
        var dictionaryForChangeOrder = new SearchConditionDict();
        var ModelsForChangeOrder = new SearchConditionModel();

        if (!string.IsNullOrWhiteSpace(ModelsForChangeOrder.OrderSelect))
        {
            dictionaryForChangeOrder.keywordOfSearch.Add("order", ModelsForChangeOrder.OrderSelect);
        }
        
        return dictionaryForChangeOrder.keywordOfSearch;
    }*/

}