using System.Text.Json;
namespace apiBukLitoprocess.Clases;

public class RestClientService
{
    
    private readonly IHttpClientFactory _httpClientFactory;
    public RestClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
     public async Task<T?> GetAsync<T>(string clientName, string url)
    {
        var client = _httpClientFactory.CreateClient(clientName);        
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    
 public async Task<TResponse?> PostAsync<TRequest, TResponse>(string clientName, string url, TRequest data)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        var response = await client.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PatchAsync<TRequest>(string clientName, string url, TRequest data)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

}
