//I will write the code to get API data which is in Narou developer and display it.
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using NovelsSearchApp.NovelModels;
using System.Net.Http.Json;
using System.Linq;
using System.Diagnostics;

namespace NovelsSearchApp.Components.Pages;

public partial class NovelsSearch : ComponentBase
{
    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } =default!;//what is default! mean?

    public IEnumerable<NovelModel>? novels;

    public bool IsGettingAPISuccess {get; set; }

    public bool IsSearchbuttonpushed {get; set; } = false;

    protected async Task DisplayResults()//"Network error" is occurred because this method is exchanged for `OnInitializedAsync` method. I should refer to the chat with Gemini named "BlazorでのAPI連携と画面表示" 
    {
        IsSearchbuttonpushed = true;
        try
        {
            var httpClient = HttpClientFactory.CreateClient("NarouAPI");
            using HttpResponseMessage response = await httpClient.GetAsync("novelapi/api/?out=json&lim=10&order=hyoka&of=t-w");//This arugment is for testing.

            //If network can be connected but API return eroor, this IF statement will catch it.
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

    //OnResearch = "@DisplaySearchResults";
}