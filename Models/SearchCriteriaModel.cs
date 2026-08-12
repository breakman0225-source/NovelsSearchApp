//This Area is used for using directive.

namespace NovelsSearchApp.Models;

public class SearchConditionModel
{
    //Properties for search criteria.
    public string? HighPriorityCriteria { get; set; }  = default!;
    public string? LowPriorityCriteria { get; set; } = default!;
    public string? Exceptword { get; set; } = default!;
    //Properties for Displaying novels option.
    public string OrderSelect { get; set; } = default!;
    public string LimSelect { get; set; } = default!;
}