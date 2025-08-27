using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Framework.Net8;
using StockService.Net8.Services.AuthenticationServices;
using StockService.Net8.Services.TransactionServices;

namespace StockService.Net8;

public static class StockServiceExtension
{
    public static IServiceCollection AddStockService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            //.Configure<AppSetting>(configuration.GetSection("Configuration"))
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IBuyStockService, BuyStockService>()
            .AddTransient<ISellStockService, SellStockService>();
        
        return services;
    }
}
