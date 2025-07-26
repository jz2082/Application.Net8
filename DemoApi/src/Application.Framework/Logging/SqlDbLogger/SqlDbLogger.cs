using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class SqlDbLogger : ILogger
{
    private readonly string _name;
    private readonly SqlDbLoggerOptions _options;
    private Func<object, Exception, string> _appLoggerFormatter;
    private readonly SqlLoggerDbContext _loggerDbContext;

    public SqlDbLogger(string name, SqlDbLoggerOptions options, SqlLoggerDbContext loggerDbContext)
    {
        _name = name;
        _options = options;
        _loggerDbContext = loggerDbContext;
    }

    #region ILogger Implementation

    /// <summary>
    ///     Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <param name="state">The identifier for the scope.</param>
    /// <returns>
    ///     An IDisposable that ends the logical operation scope on dispose.
    /// </returns>
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    /// <summary>
    ///     Determines whether the logging level is enabled.
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <returns>The <see cref="bool"/> value indicating whether the logging level is enabled.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _options.LogLevel;
    }

    /// <summary>
    ///     Logs an exception into the log.
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <param name="eventId">The event Id.</param>
    /// <param name="state">The state.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="formatter">The formatter.</param>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <exception cref="ArgumentNullException">Throws when the <paramref name="formatter"/> is null.</exception>

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        var logInfo = new AdditionalInfo();
        if (_appLoggerFormatter != null)
        {
            logInfo = new AdditionalInfo(_appLoggerFormatter(state, exception));
        }
        else
        {
            logInfo.Add(AppLogger.Message, state?.ToString());
        }
        logInfo.Add("LoggerName", _name);
        logInfo.Add("Level", AppLogUtility.GetAppLoggerLevel(logLevel));
        // save to Database
        var loggerInfo = new LoggerInfoEntity(logInfo);
        loggerInfo.Environment = _options.Environment;
        loggerInfo.Flag = false;
        if (_loggerDbContext != null)
        {
            _loggerDbContext.LoggerInfo.Add(loggerInfo);
            _loggerDbContext.SaveChanges();
        }
    }

    #endregion

    /// <summary>
    ///     Define AppLogger formatter.
    /// </summary>
    /// <param name="formatter">The formatting function to be used</param>
    /// <returns> The logger itself for fluent use. </returns>
    public SqlDbLogger UsingAppLoggerFormatter(Func<object, Exception, string> formatter)
    {
        _appLoggerFormatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        return this;
    }
}
