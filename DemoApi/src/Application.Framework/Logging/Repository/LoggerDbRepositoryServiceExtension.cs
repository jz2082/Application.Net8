using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Application.Framework.Logging;

public static partial class LoggerDbRepositoryServiceExtension
{
    public static IServiceCollection AddLoggerDbRepositoryService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<SqlLoggerDbContext>(options =>
                options.UseSqlServer(
                    configuration["SqlDbLogger:ConnectionString"],
                    sqlOptions =>
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)))
            .AddTransient<BaseEntityRepository<LoggerInfoEntity>, LoggerInfoRepository>();
        return services;
    }
}