using System.Collections;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Application.Framework.Logging;

public static class AppLogUtility
{
    public static bool IsValidJson(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        try
        {
            var json = JContainer.Parse(value);
            return true;
        }
        catch
        {
            // ignore the error
        }
        return false;
    }

    public static string GetAppLoggerLevel(LogLevel logLevel) => logLevel switch
    {
        LogLevel.Trace => "INFO",
        LogLevel.Debug => "DEBUG",
        LogLevel.Information => "INFO",
        LogLevel.Warning => "WARN",
        LogLevel.Error => "ERROR",
        LogLevel.Critical => "FATAL",
        _ => "INFO"
    };


    /// <summary>
    ///     Get Exception Description.
    /// </summary>
    public static string GetExceptionMessage(Exception ex)
    {
        var returnValue = string.Empty;
        if (ex == null) return (returnValue);
        returnValue = ex.Message;
        if (ex.InnerException != null)
        {
            returnValue = string.Concat(returnValue, Environment.NewLine, GetExceptionMessage(ex.InnerException));
        }
        return (returnValue);
    }

    /// <summary>
    ///     Get Exception Description.
    /// </summary>
    public static IEnumerable<string> GetExceptionMessageList(Exception ex)
    {
        List<string> returnValueList = new List<string>();
        if (ex != null)
        {
            returnValueList.Add(ex.Message);
            if (ex.InnerException != null)
            {
                returnValueList.AddRange(GetExceptionMessageList(ex.InnerException));
            }
        }
        return (returnValueList);
    }

    /// <summary>
    ///     Get Exception Description.
    /// </summary>
    public static string GetExceptionDescription(Exception ex, bool hasInnerException = false)
    {
        if (ex == null) return (string.Empty);
        var exBuilder = new StringBuilder();
        var exceptionType = ex.GetType().Name;
        var hasLocalInnerException = ex.InnerException != null;
        if (!string.IsNullOrEmpty(ex.Source))
        {
            exBuilder.AppendFormat("{0}.Source = {1}{2}", exceptionType, ex.Source, Environment.NewLine);
        }
        exBuilder.AppendFormat("{0}.Message = {1}{2}", exceptionType, ex.Message, Environment.NewLine);
        if (ex.Data.Count > 0)
        {
            int count = 0;
            exBuilder.AppendFormat("{0}.Data Extra details: ---{1}", exceptionType, Environment.NewLine);
            foreach (DictionaryEntry de in ex.Data)
            {
                object key = de.Key;
                if (key is Guid)
                {
                    exBuilder.AppendFormat("{0}{1}", de.Value, Environment.NewLine);
                }
                else
                {
                    exBuilder.AppendFormat("Key({0}):{1} = {2}{3}", count, key, de.Value, Environment.NewLine);
                }
                count++;
            }
            exBuilder.AppendFormat("--- {0}.Data Extra details End.{1}{1}", exceptionType, Environment.NewLine);
        }
        if (hasLocalInnerException)
        {
            exBuilder.AppendFormat("{0}.InnerException ---{1}", exceptionType, Environment.NewLine);
            exBuilder.AppendFormat("{0}{1}", GetExceptionDescription(ex.InnerException, hasLocalInnerException), Environment.NewLine);
        }
        exBuilder.AppendFormat("{0}{1}", ex, Environment.NewLine);
        if (hasInnerException)
        {
            exBuilder.AppendFormat("--- End of {0}.InnerException ---{1}", exceptionType, Environment.NewLine);
        }
        return exBuilder.ToString();
    }

    /// <summary>
    ///     Formats an exception.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <param name="state">The state of the logged object.</param>
    /// <param name="exception">The exception to be logged.</param>
    /// <returns>The text with the formatted message.</returns>
    public static string AppLoggerFormatter<TState>(TState state, Exception exception)
    {
        var logMessage = state?.ToString();
        var isJsonMessage = AppLogUtility.IsValidJson(logMessage);
        var logInfo = isJsonMessage ? new AdditionalInfo(logMessage ?? "") : new AdditionalInfo();
        if (!isJsonMessage) logInfo.Add(AppLogger.Message, logMessage ?? "");
        logInfo.Add("IsJsonMessage", isJsonMessage);
        logInfo.Add("LoggerId", Guid.NewGuid());
        logInfo.Add("MachineName", Environment.MachineName);
        logInfo.Add("TimeStamp", DateTime.Now);
        if (exception != null)
        {
            logInfo.Add("ExceptionMessage", exception.Message);
            logInfo.Add("ExceptionType", exception.GetType().Name);
            if (exception.Data != null)
            {
                foreach (DictionaryEntry de in exception.Data)
                {
                    var key = de.Key.ToString();
                    if (key == "Application" || key == "Module" || key == "Method" || key == "ExecutionTime")
                    {
                        logInfo.Add(key, de.Value ?? "");
                    }
                }
            }
            logInfo.Add("ExceptionInfo", GetExceptionDescription(exception));
        }
        logInfo.Add("DateCreated", DateTime.Now);
        logInfo.Add("DateModified", DateTime.Now);
        return logInfo.GetValue();
    }
}