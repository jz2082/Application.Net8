using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

public class HealthCheckRepository : BaseObject, IHealthCheckRepository
{
    private readonly HealthCheckOptionSetting _healthCheckSetting;
    private readonly IHealthCheckFactory _healthCheckFactory;

    public HealthCheckRepository(IOptions<HealthCheckOptionSetting> healthCheckSetting, IHealthCheckFactory healthCheckFactory, ILogger<HealthCheckRepository> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "HealthCheckRepository");

        _healthCheckSetting = healthCheckSetting.Value;
        _healthCheckFactory = healthCheckFactory;
    }

    public async Task<IEnumerable<HealthCheckResult>> HealthCheckAsync(CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "HealthCheckAsync");
        info.Add(AppLogger.Message, "HealthCheckRepository.HealthCheckAsync(CancellationToken cancellationToken = default)");

        var returnValue = new List<HealthCheckResult>();
        ClearViolation();
        if (HasViolation) return returnValue;
        try
        {
            var context = new HealthCheckContext()
            {
                Registration = default!
            };
            if (_healthCheckSetting.MemoryThreshold > 0)
            {
                returnValue.Add(await _healthCheckFactory.ServiceFor("MemoryHealthCheck").CheckHealthAsync(context, cancellationToken).ConfigureAwait(false));
            }
            if (!string.IsNullOrEmpty(_healthCheckSetting.HttpCheckEndPoint))
            {
                returnValue.Add(await _healthCheckFactory.ServiceFor("HttpHealthCheck").CheckHealthAsync(context, cancellationToken).ConfigureAwait(false));
            }
            if (!string.IsNullOrEmpty(_healthCheckSetting.SqlDbConnection))
            {
                returnValue.Add(await _healthCheckFactory.ServiceFor("SqlDbHealthCheck").CheckHealthAsync(context, cancellationToken).ConfigureAwait(false));
            }
            if (!string.IsNullOrEmpty(_healthCheckSetting.InMemoryDbConnection))
            {
                returnValue.Add(await _healthCheckFactory.ServiceFor("InMemoryDbHealthCheck").CheckHealthAsync(context, cancellationToken).ConfigureAwait(false));
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (returnValue);
    }

}