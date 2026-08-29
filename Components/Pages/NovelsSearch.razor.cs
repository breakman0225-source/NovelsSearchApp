//I will write the code to get API data which is in Narou developer and display it.
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using NovelsSearchApp.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Diagnostics;
using NovelsSearchApp.Components.Pages.UserInput;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Immutable;

namespace NovelsSearchApp.Components.Pages;

public partial class NovelsSearch : ComponentBase
{
    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } =default!;//what is default! mean?

    public IEnumerable<NovelModel>? novels;

    public bool IsGettingAPISuccess { get; set; }

    public bool IsSearchbuttonpushed { get; set; } = false;

    public string? CheckAPI { get; set; }
    public int CheckDisplaytimes { get; set; }

    protected SearchCriteria ChangeGetParamForAPI { get; set; } = default!;
    
    public int AllCountForPageNumber { get; set; } = default!;


    public async Task DisplayResults()
    {
        IsSearchbuttonpushed = true;

        try
        {
            Console.WriteLine("`Displayresults`method is invoked.");
            //This is handle of getting narou API.
            var httpClient = HttpClientFactory.CreateClient("NarouAPI");
            using HttpResponseMessage response = await httpClient.GetAsync(ChangeGetParamForAPI.ChangeQueryString());//Handle the return of `ChaneQueryString`.

            //`CheckAPI` is used for check the contents of URL which get the information of novels by query parameters.
            CheckAPI = ChangeGetParamForAPI.ChangeQueryString();

            //If network can be connected but API return erorr, this IF statement will catch it.
            if(response.IsSuccessStatusCode)
            {
                IsGettingAPISuccess = true;
                CheckDisplaytimes += 1;

                using var responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<NovelModel>? novelsList = await JsonSerializer.DeserializeAsync<IEnumerable<NovelModel>>(responseStream);
                if(novelsList != null && novelsList.Any())
                {
                    //Get allcount to calculate the number of all pages.
                    var allcount = novelsList.FirstOrDefault();
                    if(allcount != null)
                    {
                        AllCountForPageNumber = allcount.AllCount;//why does this code have no error?
                    }

                    //`novels` is the List for displaying on UI. First index in `novelsList` is allcount, so this isn't necessary.
                    novels = novelsList.Skip(1);
                }
            }
            else
            {
                //Handle the error case.
                Console.WriteLine($"APIからエラーが返されました。 StatusCode: {response.StatusCode}");
            }
            
        //Write handle code for exclude null value which exist in first element of novels by using LINQ.
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine($"ネットワークエラーが発生しました。{ex.Message}");
        }
        catch(TaskCanceledException ex)
        {
            Console.WriteLine($"通信がタイムアウトしました。{ex.Message}");
        }
        catch(JsonException ex)
        {
            Console.WriteLine($"JSONのデシリアライズに失敗しました。{ex.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"予期しないエラーが発生しました。{ex.Message}");
        }

    }
}