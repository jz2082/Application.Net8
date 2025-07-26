using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Framework;

public class HealthCheckFactory : IHealthCheckFactory
{
    private readonly IServiceProvider _serviceProvider;

    public HealthCheckFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IHealthCheck ServiceFor(string healthCheckName)
    {
        var healthCheckService = _serviceProvider.GetServices<IHealthCheck>();
        return healthCheckService.First<IHealthCheck>(x => x.GetType().Name == healthCheckName);
    }
}