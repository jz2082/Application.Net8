
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Framework.Net8;
using StockAppData.Net8.Services;
using StockAppData.Net8.Services.Common;
using StockService.Net8.Services;

namespace StockAppData.Net8;

public static class StockDataDbServiceExtension
{
    public static IServiceCollection AddInMemoryDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            //.Configure<AppSetting>(configuration.GetSection("Configuration"))
            .AddDbContext<StockDataInMemoryDbContext>(config => config
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(
                    databaseName: "StockDataDb",
                    options => options.EnableNullChecks(true)
                )
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
            .AddTransient<IAccountService, AccountDataService>()
            .AddTransient(typeof(NonQueryDataService<>))
            .AddTransient(typeof(IDataService<>), typeof(GenericDataService<>));

        return services;
    }
}