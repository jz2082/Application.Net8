using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

/// <summary>
///     SqlDb Command health check
/// </summary>
public class SqlDbHealthCheck: IHealthCheck
{
    protected AdditionalInfo _info = new();
    protected readonly ILogger<SqlDbHealthCheck> _logger = null;
    protected readonly HealthCheckOptionSetting _healthCheckSetting;
    protected readonly string _dbConnection = string.Empty;

    /// <summary>
    ///     SqlDb Health check 
    /// </summary>
    /// <param name="dbConnection"></param>
    public SqlDbHealthCheck(IOptions<HealthCheckOptionSetting> healthCheckSetting, ILogger<SqlDbHealthCheck> logger) 
    {
        _info.Add(AppLogger.Application, "Application.Framework.HealthCheck");
        _info.Add(AppLogger.Module, "SqlDbHealthCheck");

        _healthCheckSetting = healthCheckSetting.Value;
        _dbConnection = _healthCheckSetting.SqlDbConnection;
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
        info.Add(AppLogger.Message, $"SqlDbHealthCheck.CheckHealthAsync(HealthCheckContext context: {AppUtility.ObjectToString(context)}, CancellationToken cancellationToken)");
        info.Add("ReturnType", "HealthCheckResult");

        try
        {
            info.Add("SqlDbConnection", _dbConnection.Replace("sqldatadbserver", "XXXXXXXX").Replace("Dudu1120", "XXXXXXXX").Replace("sa", "XXXXXXXX").Replace("dbAdmin", "XXXXXXXX"));
            var sqlDbRetryoptions = new SqlRetryLogicOption()
            {
                NumberOfTries = 3,
                DeltaTime = TimeSpan.FromSeconds(3),
                MaxTimeInterval = TimeSpan.FromSeconds(20),
                TransientErrors = new List<int> { -1, -2, 0, 109, 233, 997, 1222, 10060, -2146232060, 596 }
            };
            var sqlDbRetryProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(sqlDbRetryoptions);
            using var sqlConnection = new SqlConnection()
            {
                ConnectionString = _dbConnection,
                RetryLogicProvider = sqlDbRetryProvider
            };
            await sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
            using var sqlCommand = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandText = "SELECT 1 AS VAL",
                CommandType = CommandType.Text
            };
            await sqlCommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
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