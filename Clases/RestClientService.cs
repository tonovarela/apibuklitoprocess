namespace apiBukLitoprocess.Clases;

public class RestClientService
{
     private readonly HttpClient _httpClient;

     private readonly string URL_SERVICE = "https://litoprocess.buk.mx/api/v1/mexico";
     private readonly string AUTH_TOKEN = "yourAPI";

    public RestClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        _httpClient.DefaultRequestHeaders.Add("auth_token", AUTH_TOKEN);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    

    public async Task<T?> GetAsync<T>(string url)
    {
    var request = new HttpRequestMessage(HttpMethod.Get,URL_SERVICE + url);
    var response = await _httpClient.SendAsync(request);
    // var json = await response.Content.ReadAsStringAsync();
    // Console.WriteLine(json);
    return await response.Content.ReadFromJsonAsync<T>();
    }

    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _httpClient.PostAsJsonAsync( URL_SERVICE + url, data);
        //response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

}
