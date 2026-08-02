//This Area is used for using directive.

namespace NovelsSearchApp.Models;

public class SearchConditionModel
{
    public string? HighPriorityCriteria { get; set; }  = default!;
    public string? LowPriorityCriteria { get; set; } = default!;
    public string? Exceptword { get; set; } = default!;

}