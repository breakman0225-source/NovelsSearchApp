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
}