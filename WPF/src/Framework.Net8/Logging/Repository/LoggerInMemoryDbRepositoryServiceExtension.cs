using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Application.Framework.Logging;

public static partial class LoggerInMemoryDbRepositoryServiceExtension
{
    public static IServiceCollection AddLoggerInMemoryDbRepositoryService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<InMemoryLoggerDbContext>(config => config
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(
                    databaseName: configuration["InMemoryDbLogger:ConnectionString"] ?? "DemoDb",
                    options => options.EnableNullChecks(true)
                )
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        return services;
    }
}