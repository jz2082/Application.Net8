using StockAppApiData.Net8.Results;
using StockService.Net8.Exceptions;
using StockService.Net8.Services;

namespace StockAppApiData.Net8.Services;

public class StockPriceService : IStockPriceService
{
    private readonly FinancialModelingPrepHttpClient _client;

    public StockPriceService(FinancialModelingPrepHttpClient client)
    {
        _client = client;
    }

    public async Task<double> GetPrice(string symbol)
    {
        string uri = "stock/real-time-price/" + symbol;

        StockPriceResult stockPriceResult = await _client.GetAsync<StockPriceResult>(uri);

        if (stockPriceResult.Price == 0)
        {
            throw new InvalidSymbolException(symbol);
        }

        return stockPriceResult.Price;
    }
}
