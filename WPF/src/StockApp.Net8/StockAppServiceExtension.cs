using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Framework.Net8;
using StockAppApiData.Net8.Services;
using StockService.Net8.Services;
using StockApp.Net8.State.Navigators;
using StockApp.Net8.ViewModels;

namespace StockAppApiData.Net8;

public static class StockAppServiceExtension
{
    public static IServiceCollection AddStockAppService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<AppSetting>(configuration.GetSection("Configuration"))
            .AddTransient<INavigator, Navigator>()
            .AddTransient<MainViewModel>();

        return services;
    }
}
