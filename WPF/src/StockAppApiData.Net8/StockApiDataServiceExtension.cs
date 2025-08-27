using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Framework.Net8;
using StockAppApiData.Net8.Services;
using StockService.Net8.Services;

namespace StockAppApiData.Net8;

public static class StockApiDataServiceExtension
{
    public static IServiceCollection AddStockApiDataService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<AppSetting>(configuration.GetSection("Configuration"))
            .AddTransient<FinancialModelingPrepHttpClientFactory>()
            .AddTransient<IMajorIndexService, MajorIndexService>()
            .AddTransient<IStockPriceService, StockPriceService>();
            
        return services;
    }
}
