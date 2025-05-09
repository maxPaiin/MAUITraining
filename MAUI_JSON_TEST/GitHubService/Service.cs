using System.Net.Http.Json;
using System.Text.Json;
using Pojo;

namespace MAUI_JSON_TEST.GitHubService
{
    public class Service
    {
    private readonly HttpClient _httpClient;
    
    public Service(){
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
    }

    public async Task<List<Respond>> SearchRepositoriesAsync(string query ,string sort)
    {
        var url = "https://api.github.com/search/repositories?q="+query+"&"+"sort="+sort+"&per_page="+10;
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var stream = await _httpClient.GetStreamAsync(url);
        var jsonDoc = await JsonDocument.ParseAsync(stream);
        var items = jsonDoc.RootElement.GetProperty("items");

        var list = new List<Respond>();
        foreach (var item in items.EnumerateArray())
        {
            var repo = JsonSerializer.Deserialize<Respond>(item.GetRawText(), options);
            list.Add(repo);
        }

        return list;
    }
    }
}

