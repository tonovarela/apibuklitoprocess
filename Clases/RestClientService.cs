using apiBukLitoprocess.conf;
using Microsoft.Extensions.Options;

namespace apiBukLitoprocess.Clases;

public class RestClientService
{
     private readonly HttpClient _httpClient;
     private readonly ApiSettings _settings;

     private  string URL_SERVICE = String.Empty;
     

    public RestClientService(IOptions<ApiSettings> options,HttpClient httpClient)
    {
        _httpClient = httpClient;
        _settings = options.Value;
        _httpClient.DefaultRequestHeaders.Add("auth_token", _settings.Token);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        URL_SERVICE = _settings.Url_API;
    }
    
    public async Task<T?> GetAsync<T>(string url)
    {
    var request = new HttpRequestMessage(HttpMethod.Get,URL_SERVICE + url);
    // try
    //     {
       var response = await _httpClient.SendAsync(request);
       return await response.Content.ReadFromJsonAsync<T>();
        // } catch(System.Text.Json.JsonException ex)
        // {
        //     Console.WriteLine($"Error deserializing JSON response: {ex.Message}");
        //     return default;
        // }
    }

    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _httpClient.PostAsJsonAsync( URL_SERVICE + url, data);
        //response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

}
