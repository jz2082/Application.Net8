using System.Text;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class AppConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly AppConsoleLoggerOptions _options;
    private Func<object, Exception, string> _appLoggerFormatter = null;

    public AppConsoleLogger(string name, AppConsoleLoggerOptions options)
    {
        _name = name;
        _options = options;
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
        return logLevel >= _options?.LogLevel;
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
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
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
        logInfo.Add("Environment", _options?.Environment);
        logInfo.Add("EventId", eventId.Name + eventId.Id);
        logInfo.Add("Level", AppLogUtility.GetAppLoggerLevel(logLevel));
        // log info
        string stringValue = string.Empty;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(logInfo.GetStringValue(AppLogger.Message, true));
        stringValue = logInfo.GetStringValue(AppLogger.Application, true);
        if (!string.IsNullOrEmpty(stringValue)) sb.AppendLine(string.Format("Application: {0}", stringValue));
        stringValue = logInfo.GetStringValue(AppLogger.Module, true);
        if (!string.IsNullOrEmpty(stringValue)) sb.AppendLine(string.Format("Module: {0}", stringValue));
        stringValue = logInfo.GetStringValue(AppLogger.Method, true);
        if (!string.IsNullOrEmpty(stringValue)) sb.AppendLine(string.Format("Method: {0}", stringValue));
        stringValue = logInfo.GetStringValue("ExecutionTime", true);
        if (!string.IsNullOrEmpty(stringValue) && AppUtility.ObjectToDouble(stringValue) > 0) sb.AppendLine(string.Format("ExecutionTime: {0}", stringValue));
        stringValue = logInfo.GetStringValue("ExceptionInfo", true);
        if (!string.IsNullOrEmpty(stringValue))
        {
            sb.AppendLine(stringValue);
        }
        else
        {
            // log additional data
            foreach (var item in logInfo.Value)
            {
                sb.AppendLine(string.Format("{0}: {1}", item.Key, item.Value));
            }
        }
        var color = Console.ForegroundColor;
        Console.ForegroundColor = _options != null ? _options.Color : ConsoleColor.Yellow;
        Console.WriteLine(sb.ToString());
        Console.ForegroundColor = color;
    }

    #endregion

    /// <summary>
    ///     Define AppLogger formatter.
    /// </summary>
    /// <param name="formatter">The formatting function to be used</param>
    /// <returns> The logger itself for fluent use. </returns>
    public AppConsoleLogger UsingAppLoggerFormatter(Func<object, Exception, string> formatter)
    {
        _appLoggerFormatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        return this;
    }
}
