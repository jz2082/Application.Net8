using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Framework;

public interface IHealthCheckFactory 
{
    IHealthCheck ServiceFor(string healthCheckName);
}