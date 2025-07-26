using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class SqlDbLoggerProvider : ILoggerProvider
{
    /// <summary>
    ///     The AppDatabaseLoggerProvider config file.
    /// </summary>
    private readonly SqlDbLoggerOptions _options;
    /// <summary>
    ///     The formatter Func.
    /// </summary>
    private Func<object, Exception, string> _appLoggerFormatter;
    /// <summary>
    ///     The loggers collection.
    /// </summary>
    private readonly ConcurrentDictionary<string, SqlDbLogger> _loggers = new ConcurrentDictionary<string, SqlDbLogger>();

    private readonly SqlLoggerDbContext _loggerDbContext;

    public SqlDbLoggerProvider(SqlDbLoggerOptions options)
    {
        _options = options;
        _appLoggerFormatter = AppLogUtility.AppLoggerFormatter;
        _loggerDbContext = new SqlLoggerDbContext(_options.ConnectionString);
    }

    #region ILogger ILoggerProvider

    public ILogger CreateLogger(string categoryName)
    {
        var logger = new SqlDbLogger(categoryName, _options, _loggerDbContext).UsingAppLoggerFormatter(_appLoggerFormatter);
        return _loggers.GetOrAdd(categoryName, logger);
    }

    public void Dispose()
    {
        _loggers.Clear();
        if (_loggerDbContext != null) _loggerDbContext.Dispose();
    }

    #endregion
}