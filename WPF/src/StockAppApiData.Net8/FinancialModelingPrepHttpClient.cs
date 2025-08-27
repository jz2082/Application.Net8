using Newtonsoft.Json;

namespace StockAppApiData.Net8;

public class FinancialModelingPrepHttpClient : HttpClient
{
    private readonly string _apiKey;

    public FinancialModelingPrepHttpClient(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        HttpResponseMessage response = await GetAsync($"{uri}&apikey={_apiKey}");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }
}
