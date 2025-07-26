using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public static class InMemoryDbLoggerExtension
{
    public static ILoggerFactory AddInMemoryDbLogger(this ILoggerFactory loggerFactory, InMemoryDbLoggerOptions options)
    {
        loggerFactory.AddProvider(new InMemoryDbLoggerProvider(options));
        return loggerFactory;
    }

    public static ILoggerFactory AddInMemoryDbLogger(this ILoggerFactory loggerFactory)
    {
        var options = new InMemoryDbLoggerOptions
        {
            LogLevel = LogLevel.Information,
            ConnectionString = "DemoDb"
        };
        return loggerFactory.AddInMemoryDbLogger(options);
    }

    public static ILoggerFactory AddInMemoryDbLogger(this ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        var options = configuration.Get<InMemoryDbLoggerOptions>();
        if (options == null)
        {
            options = new InMemoryDbLoggerOptions()
            {
                ConnectionString = "DemoDb"
            };
        }
        return loggerFactory.AddInMemoryDbLogger(options);
    }

    public static ILoggerFactory AddInMemoryDbLogger(this ILoggerFactory loggerFactory, Action<InMemoryDbLoggerOptions> configure)
    {
        var options = new InMemoryDbLoggerOptions();
        configure(options);
        return loggerFactory.AddInMemoryDbLogger(options);
    }
}