using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace NovelsSearchApp.NovelModels;

public class NovelModel
//I will add Properties which deal with summury and Tags and so on to this class.
{
    //[Key]
    //public int Id { get; set; }
    [JsonPropertyName("allcount")]
    public int AllCount { get; set; }

    [Display(Name = "タイトル")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [Display(Name ="作者")]
    [JsonPropertyName("writer")]
    public string? Writer { get; set; }
}