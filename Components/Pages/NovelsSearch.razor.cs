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

namespace NovelsSearchApp.Components.Pages;

public partial class NovelsSearch : ComponentBase
{
    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } =default!;//what is default! mean?

    public IEnumerable<NovelModel>? novels;

    public bool IsGettingAPISuccess { get; set; }

    public bool IsSearchbuttonpushed { get; set; } = false;

    public string? CheckAPI { get; set; }

    public string? CheckHighPriorityCriteria { get; set; }
    SearchConditionModel CheckSearchCriteria = new SearchConditionModel();
    
    [Inject]
    public SearchCriteria ChangeGetParamForAPI { get; set; } = default!;

    protected async Task DisplayResults()
    {
        IsSearchbuttonpushed = true;

        try
        {
            //This is handle of getting narou API.
            var httpClient = HttpClientFactory.CreateClient("NarouAPI");
            using HttpResponseMessage response = await httpClient.GetAsync(ChangeGetParamForAPI.ChangeQueryString());//Handle the return of `ChaneQueryString`.

            //`CheckAPI` is used for check the contents of URL which get the information of novels by query parameters.
            CheckAPI = ChangeGetParamForAPI.ChangeQueryString();

            //If network can be connected but API return erorr, this IF statement will catch it.
            if(response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<NovelModel>? novelsList = await JsonSerializer.DeserializeAsync<IEnumerable<NovelModel>>(responseStream);
                if(novelsList != null)
                {
                    novels = novelsList.Skip(1);
                }

                IsGettingAPISuccess = true;
            }
            else
            {
                //Handle the error case.
                Console.WriteLine($"APIからエラーが返されました。 StatusCode: {response.StatusCode}");
            }

            if (string.IsNullOrWhiteSpace(CheckSearchCriteria.HighPriorityCriteria))
            {
                CheckHighPriorityCriteria = "There are no contents in `HighPriorityCriteria`";
            }
            else
            {
                CheckHighPriorityCriteria = CheckSearchCriteria.HighPriorityCriteria;
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