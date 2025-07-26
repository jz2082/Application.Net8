using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Framework;

/// <summary>
///     HttpClient health check
/// </summary>
public class HttpHealthCheck : IHealthCheck
{
    protected AdditionalInfo _info = new();
    protected readonly ILogger<HttpHealthCheck> _logger = null;
    protected readonly HealthCheckOptionSetting _healthCheckSetting;
    private readonly HttpClient  _httpClient;

    /// <summary>
    ///     Http Health check 
    /// </summary>
    /// <param name="url"></param>
    public HttpHealthCheck(IHttpClientFactory httpClientFactory, IOptions<HealthCheckOptionSetting> healthCheckSetting, ILogger<HttpHealthCheck> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework.HealthCheck");
        _info.Add(AppLogger.Module, "HttpHealthCheck");

        _httpClient = httpClientFactory.CreateClient("HttpHealthCheck");
        _healthCheckSetting = healthCheckSetting.Value;
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
        info.Add(AppLogger.Message, $"HttpHealthCheck.CheckHealthAsync(HealthCheckContext context: {AppUtility.ObjectToString(context)}, CancellationToken cancellationToken)");
        info.Add("ReturnType", "HealthCheckResult");

        try
        {
            info.Add("httpClient.BaseAddress", _httpClient.BaseAddress);
            var response = await _httpClient.GetAsync(_healthCheckSetting.HttpCheckEndPoint);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseValue = JsonConvert.DeserializeObject<ApiResponse<DateTime>>(responseContent);
                info.Add("DateTime", responseValue.Data);
                return HealthCheckResult.Healthy(description: $"{_healthCheckSetting.HttpCheckEndPoint} is Healthy and StatusCode: {response.StatusCode}", data: (IReadOnlyDictionary<string, object>?)info.Value);
            }
            else
            {
                return HealthCheckResult.Degraded(description: $"{_healthCheckSetting.HttpCheckEndPoint} is Degraded and StatusCode: {response.StatusCode}", data: (IReadOnlyDictionary<string, object>?)info.Value);
            }
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