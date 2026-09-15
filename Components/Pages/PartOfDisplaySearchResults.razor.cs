//Those line is used for using directive.
using System.Runtime.CompilerServices;
using System.Security.Principal;
using NovelsSearchApp.Components.Pages.UserInput;
using NovelsSearchApp.Models;


namespace NovelsSearchApp.Components.Pages;

public partial class PartOfDisplaySearchResults
{
    private int TheNumberOfAllPages { get; set; } = default!;
    private List<int> allPageNumber = new();
    private List<int> displayPages = new();
    private IEnumerable<NovelModel>? NovelsInChildForDisplaying { get; set; }
    private string? _pagenationMethodCheck;
    private IEnumerable<NovelModel>? _lastNovelsInChild;
    //private IEnumerable<NovelModel>? novelsForDisplaying;

    //Those variable is used for check logic.
    //private int skipCount = 0;
    //private IEnumerable<NovelModel>? takeCount;

    protected override void OnParametersSet()//This method is used to match the number of novels in the first Page with `LimSelect`.
    {
        if(NovelsInChild == null || !NovelsInChild.Any())
        {
            return;
        }

        int lim = int.Parse(NovelParametersForPageCount.LimSelect);

        if (!string.IsNullOrWhiteSpace(NovelParametersForPageCount.LowPriorityCriteria))
        {   
            if(_lastNovelsInChild != NovelsInChild)//This if statement aim to specify the signal of calling `OnParametersSet` method because of prevending malfunction of other Parameter.
            {
                NovelsInChildForDisplaying = NovelsInChild.Take(lim);//This code prevend displaying all of novels in List in the first page.
            }
        }
        else
        {
            NovelsInChildForDisplaying = NovelsInChild;
        }

        _lastNovelsInChild = NovelsInChild;
        base.OnParametersSet();//This `base.onParametersSet` is not neccesary.
    }

    private int CalculateAllPages()
    {
       
        int intlim = int.Parse(NovelParametersForPageCount.LimSelect);
        int totalCount = 0;

        if (!string.IsNullOrWhiteSpace(NovelParametersForPageCount.LowPriorityCriteria))
        {
            totalCount = NovelsInChild!.Count();
        }
        else
        {
            totalCount = GetAllcountForPageNumber;
        }

        TheNumberOfAllPages = (int)Math.Ceiling((double)totalCount / intlim);
        
        return TheNumberOfAllPages;
    }

    //This is formula to adjust the number of pages. Display only the pages within five pages of the current page.
    private List<int> MakePageList()
    {
        //This is handle to make List for Page.
        int allPages = CalculateAllPages();
        int lim = int.Parse(NovelParametersForPageCount.LimSelect);
        
        int pageLimit = 2000 / lim;

        //This if statement is used in the second time search because the results of the first search remains in `allPageNumber` List.
        if(allPageNumber.Any())
        {
            allPageNumber.Clear();
        }
        
        for(int i=1; i<=allPages&i<=pageLimit; i++)
        {
            allPageNumber.Add(i);
        }
        
        return allPageNumber;
    }
    
    //This process sets the upper and lower limits for the pages to be displayed.
    private List<int> MakeDisplayPages()
    {
        if (displayPages.Any())
        {
            displayPages.Clear();
        }
        
        foreach(var displayPage in MakePageList())
        {
            if(displayPage <= NovelParametersForPageCount.CurrentPageNumber + 5 &&
            displayPage >= NovelParametersForPageCount.CurrentPageNumber - 5)
            {
                displayPages.Add(displayPage);
            }
        }
        
        return displayPages;
    }

    private async Task HandlePagenationAsync(int pageNumber)
    {
        int lim = int.Parse(NovelParametersForPageCount.LimSelect);

        if (!string.IsNullOrWhiteSpace(NovelParametersForPageCount.LowPriorityCriteria))
        {
            NovelsInChildForDisplaying = NovelsInChild!.Skip(lim * (pageNumber - 1)).Take(lim);

            NovelParametersForPageCount.CurrentPageNumber = pageNumber;

        }
        else
        {
            NovelParametersForPageCount.CurrentPageNumber = pageNumber;

            if (OnPagenationParam.HasDelegate)
            {
                await OnPagenationParam.InvokeAsync();
                //`await` is needed because delegated method `DisplayResults` has the function to get API, with network communication.
                _pagenationMethodCheck = "The method is invoked.";
            }
        }
    }

    /*private int NovelsIndex()
    {
        if(NovelParametersForPageCount.CurrentPageNumber == 1)
        {
            
        }
    }*/

    private string GetNovelURL(string ncode)
    {
        var baseUriForNcode = "https://ncode.syosetu.com/";
        var novelURL = baseUriForNcode + $"{ncode.ToLower()}/";

        return novelURL;
    }

}