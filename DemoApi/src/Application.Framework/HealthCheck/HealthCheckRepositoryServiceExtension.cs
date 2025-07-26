using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Application.Framework;

public static partial class HealthCheckRepositoryServiceExtension
{
    public static IServiceCollection AddHealthCheckRepositoryService(this IServiceCollection services, IConfiguration configuration)
    {
        var pollyOptionsSettings = configuration.GetSection("PollyOptions").Get<PollyOptionSetting>();
        services
           .AddHttpClient("HttpHealthCheck", httpClient =>
           {
               httpClient.DefaultRequestHeaders.Accept.Clear();
               httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               httpClient.BaseAddress = new Uri(configuration["HealthCheckOptions:HttpCheckBaseAddress"]);
           })
           .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(pollyOptionsSettings.RetryCount,
                    retryAttempt => TimeSpan.FromMilliseconds(pollyOptionsSettings.RetryDelayMilliSeconds))
           )
           .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(pollyOptionsSettings.NumberOfBreakEvent,
                    TimeSpan.FromMilliseconds(pollyOptionsSettings.DurationOfBreak))
           );


        _ = services
            .Configure<PollyOptionSetting>(configuration.GetSection("PollyOptions"))
            .Configure<HealthCheckOptionSetting>(configuration.GetSection("HealthCheckOptions"))
            .AddTransient<IHealthCheck, MemoryHealthCheck>()
            .AddTransient<IHealthCheck, HttpHealthCheck>()
            .AddTransient<IHealthCheck, SqlDbHealthCheck>()
            .AddTransient<IHealthCheck, InMemoryDbHealthCheck>()
            .AddTransient<IHealthCheckFactory, HealthCheckFactory>()
            .AddTransient<IHealthCheckRepository, HealthCheckRepository>(); 
        return services;
    }
}
