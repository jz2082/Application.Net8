using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockData.Net8.Services;
using StockData.Net8.Services.Common;
using StockService.Net8.Services;

namespace StockData.Net8;

public static class StockDataDbServiceExtension
{
    public static IServiceCollection AddInMemoryDbService(this IServiceCollection services)
    {
        services
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