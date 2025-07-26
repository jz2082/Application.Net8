using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public static class SqlDbLoggerExtension
{
    public static ILoggerFactory AddSqlDbLogger(this ILoggerFactory loggerFactory, SqlDbLoggerOptions options)
    {
        loggerFactory.AddProvider(new SqlDbLoggerProvider(options));
        return loggerFactory;
    }

    public static ILoggerFactory AddSqlDbLogger(this ILoggerFactory loggerFactory)
    {
        var options = new SqlDbLoggerOptions
        {
            LogLevel = LogLevel.Information
        };
        return loggerFactory.AddSqlDbLogger(options);
    }

    public static ILoggerFactory AddSqlDbLogger(this ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        var options = configuration.Get<SqlDbLoggerOptions>();
        return loggerFactory.AddSqlDbLogger(options);
    }

    public static ILoggerFactory AddSqlDbLogger(this ILoggerFactory loggerFactory, Action<SqlDbLoggerOptions> configure)
    {
        var options = new SqlDbLoggerOptions();
        configure(options);
        return loggerFactory.AddSqlDbLogger(options);
    }
}