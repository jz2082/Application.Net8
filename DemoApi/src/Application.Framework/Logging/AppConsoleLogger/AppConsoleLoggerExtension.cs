using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public static class AppConsoleLoggerExtension
{
    public static ILoggerFactory AddAppConsoleLogger(this ILoggerFactory loggerFactory, AppConsoleLoggerOptions? options)
    {
        loggerFactory.AddProvider(new AppConsoleLoggerProvider(options));
        return loggerFactory;
    }

    public static ILoggerFactory AddAppConsoleLogger(this ILoggerFactory loggerFactory)
    {
        var options = new AppConsoleLoggerOptions
        {
            LogLevel = LogLevel.Information,
            Color = ConsoleColor.Red
        };
        return loggerFactory.AddAppConsoleLogger(options);
    }

    public static ILoggerFactory AddAppConsoleLogger(this ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        var options = configuration.Get<AppConsoleLoggerOptions>();
        return loggerFactory.AddAppConsoleLogger(options);
    }

    public static ILoggerFactory AddAppConsoleLogger(this ILoggerFactory loggerFactory, Action<AppConsoleLoggerOptions> configure)
    {
        var options = new AppConsoleLoggerOptions();
        configure(options);
        return loggerFactory.AddAppConsoleLogger(options);
    }
}
