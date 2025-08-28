using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Framework.Net8.Logging;

public class InMemoryDbLoggerProvider : ILoggerProvider
{
    /// <summary>
    ///     InMemoryDbCommandLoggerProvider config file.
    /// </summary>
    private readonly InMemoryDbLoggerOptions _options;
    /// <summary>
    ///     The formatter Func.
    /// </summary>
    private readonly Func<object, Exception, string> _appLoggerFormatter = AppLogUtility.AppLoggerFormatter;
    /// <summary>
    ///     The loggers collection.
    /// </summary>
    private readonly ConcurrentDictionary<string, InMemoryDbLogger> _loggers = new();

    private readonly InMemoryLoggerDbContext _loggerDbContext;

    public InMemoryDbLoggerProvider(IConfiguration configuration)
    {
        var options = configuration.GetSection("InMemoryDbLogger").Get<InMemoryDbLoggerOptions>() ??
            new InMemoryDbLoggerOptions()
            {
                ConnectionString = "DemoDb"
            };
        _loggerDbContext = new InMemoryLoggerDbContext(_options.ConnectionString);
    }

    public InMemoryDbLoggerProvider(InMemoryDbLoggerOptions options)
    {
        _options = options;
        _loggerDbContext = new InMemoryLoggerDbContext(_options.ConnectionString);
    }

    #region ILogger ILoggerProvider

    public ILogger CreateLogger(string categoryName)
    {
        var logger = new InMemoryDbLogger(categoryName, _options, _loggerDbContext).UsingAppLoggerFormatter(_appLoggerFormatter);
        return _loggers.GetOrAdd(categoryName, logger);
    }

    public void Dispose()
    {
        _loggers.Clear();
        if (_loggerDbContext != null) _loggerDbContext.Dispose();
    }

    #endregion
}