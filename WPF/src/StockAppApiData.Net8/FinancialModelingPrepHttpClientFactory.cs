using Framework.Net8;
using Microsoft.Extensions.Options;

namespace StockAppApiData.Net8;

public class FinancialModelingPrepHttpClientFactory
{
    protected readonly AppSetting _appSetting;

    public FinancialModelingPrepHttpClientFactory(IOptions<AppSetting> appSetting)
    {
        _appSetting = appSetting.Value;
    }

    public FinancialModelingPrepHttpClient CreateHttpClient()
    {    
        return new FinancialModelingPrepHttpClient(_appSetting.FinancialModelingApiKey);
    }
}
