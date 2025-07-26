using System.Collections.Concurrent;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class SqlDbCommandLoggerProvider : ILoggerProvider
{
    /// <summary>
    ///     The AppDatabaseLoggerProvider config file.
    /// </summary>
    private readonly SqlDbLoggerOptions _options;
    /// <summary>
    ///     The formatter Func.
    /// </summary>
    private readonly Func<object, Exception, string> _appLoggerFormatter = AppLogUtility.AppLoggerFormatter;
    /// <summary>
    ///     The loggers collection.
    /// </summary>
    private readonly ConcurrentDictionary<string, SqlDbCommandLogger> _loggers = new();

    private readonly SqlConnection _loggerDbConnection;

    public SqlDbCommandLoggerProvider(IConfiguration configuration)
    {
        _options = configuration.GetSection("SqlDbLogger").Get<SqlDbLoggerOptions>();
        // Create a retry logic provider
        var sqlDbRetryProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(new SqlRetryLogicOption()
        {
            NumberOfTries = _options.NumberOfTries,
            DeltaTime = TimeSpan.FromSeconds(_options.DeltaTime),
            MaxTimeInterval = TimeSpan.FromSeconds(_options.MaxTimeInterval)
        });
        _loggerDbConnection = new SqlConnection()
        {
            ConnectionString = _options.ConnectionString,
            RetryLogicProvider = sqlDbRetryProvider
        };
        _loggerDbConnection.Open();
    }

    public SqlDbCommandLoggerProvider(SqlDbLoggerOptions options)
    {
        _options = options;
        // Create a retry logic provider
        var sqlDbRetryProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(new SqlRetryLogicOption()
        {
            NumberOfTries = _options.NumberOfTries,
            DeltaTime = TimeSpan.FromSeconds(_options.DeltaTime),
            MaxTimeInterval = TimeSpan.FromSeconds(_options.MaxTimeInterval)
        });
        _loggerDbConnection = new SqlConnection()
        {
            ConnectionString = _options.ConnectionString,
            RetryLogicProvider = sqlDbRetryProvider
        };
        _loggerDbConnection.Open();
    }

    #region ILogger ILoggerProvider

    public ILogger CreateLogger(string categoryName)
    {
        var logger = new SqlDbCommandLogger(categoryName, _options, _loggerDbConnection).UsingAppLoggerFormatter(_appLoggerFormatter);
        return _loggers.GetOrAdd(categoryName, logger);
    }

    public void Dispose()
    {
        _loggers.Clear();
        if (_loggerDbConnection != null) _loggerDbConnection.Dispose();
    }

    #endregion
}