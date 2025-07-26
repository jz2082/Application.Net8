using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class SqlDbCommandLogger : ILogger
{
    private readonly string _name;
    private readonly SqlDbLoggerOptions _options;
    private Func<object, Exception, string> _appLoggerFormatter = null;
    private readonly SqlConnection _loggerDbConnection;

    public SqlDbCommandLogger(string name, SqlDbLoggerOptions options, SqlConnection loggerDbConnection)
    {
        _name = name;
        _options = options;
        _loggerDbConnection = loggerDbConnection;
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
        logInfo.Add("Level", AppLogUtility.GetAppLoggerLevel(logLevel));
        // save to Database
        var loggerInfo = new LoggerInfoEntity(logInfo);
        loggerInfo.Environment = _options?.Environment;
        loggerInfo.Flag = false;
        // insert to SqlDb using sql script
        var tsqlText = "INSERT INTO [LoggerInfo] ([Id],[DateCreated],[DateModified],[Environment],[MachineName],[LoggerName],[Level],[ThreadName],[TimeStamp],[Application],[Module],[Method],[UserName],[ExecutionTime],[IsJsonMessage],[Message],[RenderedType],[RenderedInfo],[ExceptionMessage],[ExceptionType],[ExceptionInfo],[Flag]) ";
        tsqlText = string.Concat(tsqlText, "VALUES (@Id,@DateCreated,@DateModified,@Environment,@MachineName,@LoggerName,@Level,@ThreadName,@TimeStamp,@Application,@Module,@Method,@UserName,@ExecutionTime,@IsJsonMessage,@Message,@RenderedType,@RenderedInfo,@ExceptionMessage,@ExceptionType,@ExceptionInfo,0)"); 
        using var sqlCommand = new SqlCommand(tsqlText, _loggerDbConnection);
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.CommandTimeout = 120;
        sqlCommand.Parameters.Clear();
        sqlCommand.Parameters.Add(new SqlParameter("Id", loggerInfo.Id));
        sqlCommand.Parameters.Add(new SqlParameter("DateCreated", loggerInfo.DateCreated));
        sqlCommand.Parameters.Add(new SqlParameter("DateModified", loggerInfo.DateModified));
        sqlCommand.Parameters.Add(new SqlParameter("Environment", loggerInfo.Environment));
        sqlCommand.Parameters.Add(new SqlParameter("MachineName", loggerInfo.MachineName));
        sqlCommand.Parameters.Add(new SqlParameter("LoggerName", loggerInfo.LoggerName));
        sqlCommand.Parameters.Add(new SqlParameter("Level", loggerInfo.Level));
        sqlCommand.Parameters.Add(new SqlParameter("ThreadName", loggerInfo.ThreadName));
        sqlCommand.Parameters.Add(new SqlParameter("TimeStamp", loggerInfo.TimeStamp));
        sqlCommand.Parameters.Add(new SqlParameter("Application", loggerInfo.Application));
        sqlCommand.Parameters.Add(new SqlParameter("Module", loggerInfo.Module));
        sqlCommand.Parameters.Add(new SqlParameter("Method", loggerInfo.Method));
        sqlCommand.Parameters.Add(new SqlParameter("UserName", loggerInfo.UserName));
        sqlCommand.Parameters.Add(new SqlParameter("ExecutionTime", loggerInfo.ExecutionTime));
        sqlCommand.Parameters.Add(new SqlParameter("IsJsonMessage", loggerInfo.IsJsonMessage));
        sqlCommand.Parameters.Add(new SqlParameter("Message", loggerInfo.Message));
        sqlCommand.Parameters.Add(new SqlParameter("RenderedType", loggerInfo.RenderedType));
        sqlCommand.Parameters.Add(new SqlParameter("RenderedInfo", loggerInfo.RenderedInfo));
        sqlCommand.Parameters.Add(new SqlParameter("ExceptionMessage", loggerInfo.ExceptionMessage));
        sqlCommand.Parameters.Add(new SqlParameter("ExceptionType", loggerInfo.ExceptionType));
        sqlCommand.Parameters.Add(new SqlParameter("ExceptionInfo", loggerInfo.ExceptionInfo));
        sqlCommand.ExecuteNonQuery();
    }

    #endregion

    /// <summary>
    ///     Define AppLogger formatter.
    /// </summary>
    /// <param name="formatter">The formatting function to be used</param>
    /// <returns> The logger itself for fluent use. </returns>
    public SqlDbCommandLogger UsingAppLoggerFormatter(Func<object, Exception, string> formatter)
    {
        _appLoggerFormatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        return this;
    }
}
