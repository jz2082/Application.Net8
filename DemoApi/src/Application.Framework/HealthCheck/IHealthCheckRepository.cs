using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Framework;

public interface IHealthCheckRepository
{
    Task<IEnumerable<HealthCheckResult>> HealthCheckAsync(CancellationToken cancellationToken = default);
}