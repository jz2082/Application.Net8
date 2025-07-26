using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class AppConsoleLoggerProvider : ILoggerProvider
{
    /// <summary>
    ///     config object
    /// </summary>
    private readonly AppConsoleLoggerOptions _options;

    /// <summary>
    ///     The formatter Func.
    /// </summary>
    private Func<object, Exception, string> _appLoggerFormatter = AppLogUtility.AppLoggerFormatter;

    /// <summary>
    ///     The loggers collection.
    /// </summary>
    private readonly ConcurrentDictionary<string, AppConsoleLogger> _loggers = new ConcurrentDictionary<string, AppConsoleLogger>();

    public AppConsoleLoggerProvider(IConfiguration configuration)
    {
        _options = configuration.GetSection("AppConsoleLogger").Get<AppConsoleLoggerOptions>();
    }

    public AppConsoleLoggerProvider(AppConsoleLoggerOptions options)
    {
        _options = options;
    }

    #region ILogger ILoggerProvider

    public ILogger CreateLogger(string categoryName)
    {
        var logger = new AppConsoleLogger(categoryName, _options).UsingAppLoggerFormatter(_appLoggerFormatter);
        return _loggers.GetOrAdd(categoryName, logger);
    }

    public void Dispose()
    {
        _loggers.Clear();
    }

    #endregion
}

