using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

/// <summary>
///     SqlDb Command health check
/// </summary>
public class InMemoryDbHealthCheck : IHealthCheck
{
    protected AdditionalInfo _info = new();
    protected readonly ILogger<SqlDbHealthCheck> _logger = null;
    protected readonly HealthCheckOptionSetting _healthCheckSetting;
    protected readonly string _dbConnection = string.Empty;

    /// <summary>
    ///     SqlDb Health check 
    /// </summary>
    /// <param name="dbConnection"></param>
    public InMemoryDbHealthCheck(IOptions<HealthCheckOptionSetting> healthCheckSetting, ILogger<SqlDbHealthCheck> logger) 
    {
        _info.Add(AppLogger.Application, "Application.Framework.HealthCheck");
        _info.Add(AppLogger.Module, "InMemoryDbHealthCheck");

        _healthCheckSetting = healthCheckSetting.Value;
        _dbConnection = _healthCheckSetting.InMemoryDbConnection;
    }

    /// <summary>
    ///     Method use to generate the health report
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "CheckHealthAsync");
        info.Add(AppLogger.Message, $"InMemoryDbHealthCheck.CheckHealthAsync(HealthCheckContext context: {AppUtility.ObjectToString(context)}, CancellationToken cancellationToken)");
        info.Add("ReturnType", "HealthCheckResult");

        try
        {
            info.Add("InMemoryDbConnection", _dbConnection);
            var dbContext = new InMemoryDbHealthCheckContext(_dbConnection);
            await dbContext.Database.ExecuteSqlAsync($"SELECT 1 AS VAL").ConfigureAwait(false);
            return HealthCheckResult.Healthy(description: "Database is available", data: (IReadOnlyDictionary<string, object>?)info.Value);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            return new HealthCheckResult(status: HealthStatus.Unhealthy, description: ex.Message, data: (IReadOnlyDictionary<string, object>?)info.Value);
        }
    }
}