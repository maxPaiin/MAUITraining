using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using Pojo;

namespace MAUI_JSON_TEST.GitHubService
{
    public class Service
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public Service(){
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Respond>> SearchRepositories(string query ,string sort,Action<double> progressCallback)
    {
        var url = "https://api.github.com/search/repositories?q="+query+"&"+"sort="+sort+"&per_page="+10;
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _httpClient.SendAsync(request,HttpCompletionOption.ResponseContentRead);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new IOException("Http response:" +response.StatusCode);
        }
        
        var contentLength = response.Content.Headers.ContentLength ?? -1L;
        using var stream = await response.Content.ReadAsStreamAsync();
        using var ms = new MemoryStream();
        var buffer = new byte[8192];
        long totalRead = 0;
        int read;
        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            await ms.WriteAsync(buffer, 0, read);
            totalRead += read;

            if (contentLength > 0)
            {
                double progress = (double)totalRead / contentLength;
                progressCallback(progress);
            }
        }

        var json = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        using var jsonDoc = JsonDocument.Parse(json);

        var results = new List<Respond>();
        var items = jsonDoc.RootElement.GetProperty("items");

        foreach (var item in items.EnumerateArray())
        {
            var repo = JsonSerializer.Deserialize<Respond>(item.GetRawText(), _jsonSerializerOptions);
            results.Add(repo);
        }
        return results;
    }
    }
}
