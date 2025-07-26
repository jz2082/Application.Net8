using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using InMemoryDb.DataEntity;
using Microsoft.EntityFrameworkCore;

using Application.Framework;

namespace Demo.InMemoryDb.Repository;

public static partial class InMemoryDbRepositoryServiceExtension
{
    public static IServiceCollection AddInMemoryDbRepositoryService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<AppSetting>(configuration.GetSection("Configuration"))
            .AddDbContext<InMemoryDbContext>(config => config
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(
                    databaseName: configuration.GetSection("Configuration:DatabaseName").Value?.ToString() ?? "DemoDb", 
                    options => options.EnableNullChecks(true)
                )
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
            .AddTransient<IBidRepository, BidRepository>()
            .AddTransient<IHouseRepository, HouseRepository>()
            .AddTransient<ISpeakerRepository, SpeakerRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IProductRepository, ProductRepository>();
        return services;
    }
}