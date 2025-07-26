using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

/// <summary>
///     Memory health check
/// </summary>
public class MemoryHealthCheck : IHealthCheck
{
    protected AdditionalInfo _info = new();
    protected readonly ILogger<MemoryHealthCheck> _logger = null;
    protected readonly HealthCheckOptionSetting _healthCheckSetting;
    protected readonly long _threshold;

    /// <summary>
    ///     Memory Health check 
    /// </summary> 
    public MemoryHealthCheck(IOptions<HealthCheckOptionSetting> healthCheckSetting, ILogger<MemoryHealthCheck> logger) 
    {
        _info.Add(AppLogger.Application, "Application.Framework.HealthCheck");
        _info.Add(AppLogger.Module, "MemoryHealthCheck");
        // Default Threshold = 1024L * 1024L * 1024L = 1073741824;

        _healthCheckSetting = healthCheckSetting.Value;
        _threshold = (_healthCheckSetting != null && _healthCheckSetting.MemoryThreshold > 0) ? _healthCheckSetting.MemoryThreshold : 1073741824;
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
        info.Add(AppLogger.Message, $"MemoryHealthCheck.CheckHealthAsync(HealthCheckContext context: {AppUtility.ObjectToString(context)}, CancellationToken cancellationToken)");
        info.Add("ReturnType", "HealthCheckResult");

        try
        {
            var allocatedMemory = GC.GetTotalMemory(forceFullCollection: false);
            info.Add("AllocatedBytes", allocatedMemory);
            info.Add("Gen0Collection", GC.CollectionCount(0));
            info.Add("Gen1Collection", GC.CollectionCount(1));
            info.Add("Gen2Collection", GC.CollectionCount(2));
            var status = allocatedMemory < _threshold ? "Healthy" : "Degraded";
            var healthcheckResult = new HealthCheckResult(
                status: allocatedMemory < _threshold ? HealthStatus.Healthy : HealthStatus.Degraded,
                description: $"Memory Allocation is {status}.",
                exception: null,
                (IReadOnlyDictionary<string, object>?)info.Value);
            return await Task.FromResult(healthcheckResult);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            return new HealthCheckResult(
                status: HealthStatus.Unhealthy, 
                description: ex.Message, 
                data: (IReadOnlyDictionary<string, object>?)info.Value);
        }
    }
}
