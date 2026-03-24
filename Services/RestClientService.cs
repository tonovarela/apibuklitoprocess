using apiBukLitoprocess.conf;
using Microsoft.Extensions.Options;
using System.Text.Json;

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
        var request = new HttpRequestMessage(HttpMethod.Get, URL_SERVICE + url);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode(); // Asegura que la respuesta fue exitos        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        try
        {
            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException )    
        {            
            throw; // Vuelve a lanzar la excepción 
        }
    }

    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        
        var response = await _httpClient.PostAsJsonAsync( URL_SERVICE + url, data);    
        response.EnsureSuccessStatusCode();         
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }


    public async Task PatchAsync<TRequest>(string url, TRequest data)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, URL_SERVICE + url)
        {
            Content = JsonContent.Create(data)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

}
