using Newtonsoft.Json;
using StockService.Net8.Models;
using StockService.Net8.Services;

namespace StockAppApiData.Net8.Services;

public class MajorIndexService(FinancialModelingPrepHttpClientFactory httpClientFactory) : IMajorIndexService
{
    private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
    {
        string uri = "https://financialmodelingprep.com/stable/quote-short?symbol=" + GetUriSuffix(indexType);
        using var httpClient = _httpClientFactory.CreateHttpClient();
        var majorIndexs = await httpClient.GetAsync<IEnumerable<MajorIndex>>(uri); 
        var majorIndex = majorIndexs?.FirstOrDefault() ?? new MajorIndex();
        majorIndex.Type = indexType;
        return majorIndex;
    }

    private string GetUriSuffix(MajorIndexType indexType)
    {
        switch (indexType)
        {
            case MajorIndexType.DowJones:
                return "^DJI";
            case MajorIndexType.Nasdaq:
                return "^IXIC";
            case MajorIndexType.SP500:
                return "^GSPC";
            default:
                throw new Exception("MajorIndexType does not have a suffix defined.");
        }
    }
}
