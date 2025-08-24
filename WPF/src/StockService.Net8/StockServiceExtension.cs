using Microsoft.Extensions.DependencyInjection;
using StockService.Net8.Services.AuthenticationServices;
using StockService.Net8.Services.TransactionServices;

namespace StockService.Net8;

public static class StockServiceExtension
{
    public static IServiceCollection AddStockService(this IServiceCollection services)
    {
        services
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IBuyStockService, BuyStockService>()
            .AddTransient<ISellStockService, SellStockService>();
        
        return services;
    }
}
