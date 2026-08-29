//Those line is used for using directive.
using System.Runtime.CompilerServices;
using NovelsSearchApp.Components.Pages.UserInput;
using NovelsSearchApp.Models;


namespace NovelsSearchApp.Components.Pages;

public partial class PartOfDisplaySearchResults
{
    private int TheNumberOfAllPages { get; set; } = default!;
    private List<int> allPageNumber = new();
    private List<int> displayPages = new();
    //private List<int> displayPagesForUI = new();
    private string? _pagenationMethodCheck;

    private int CalculateAllPages()
    {
        if(NovelParametersForPageCount.LimSelect != null)
        {
            TheNumberOfAllPages = GetAllcountForPageNumber / int.Parse(NovelParametersForPageCount.LimSelect);
        }
        else
        {
            return 0;
        }
        
        return TheNumberOfAllPages;
    }

    //This is formula to adjust the number of pages. Display only the pages within five pages of the current page.
    private List<int> MakePageList()
    {
        //This is handle to make List for Page.
        int allPages = CalculateAllPages();

        if(allPageNumber.Any())
        {
            allPageNumber.Clear();
        }

        if(allPages % int.Parse(NovelParametersForPageCount.LimSelect) != 0)
        {
            allPages += 1;
        }
        
        for(int i=1; i<=allPages; i++)
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
        NovelParametersForPageCount.CurrentPageNumber = pageNumber;

        if (OnPagenationParam.HasDelegate)
        {
            await OnPagenationParam.InvokeAsync();
            //`await` is needed because delegated method `DisplayResults` has the function to get API, with network communication.
            _pagenationMethodCheck = "The method is invoked.";
        }
    }

    private string GetNovelURL(string ncode)
    {
        var baseUriForNcode = "https://ncode.syosetu.com/";
        var novelURL = baseUriForNcode + $"{ncode.ToLower()}/";

        return novelURL;
    }

}