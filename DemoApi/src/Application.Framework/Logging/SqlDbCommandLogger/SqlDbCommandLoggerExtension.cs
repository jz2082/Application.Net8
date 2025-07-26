using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public static class SqlDbCommandLoggerExtension
{
    public static ILoggerFactory AddSqlDbCommandLogger(this ILoggerFactory loggerFactory, SqlDbLoggerOptions? options)
    {
        loggerFactory.AddProvider(new SqlDbCommandLoggerProvider(options));
        return loggerFactory;
    }

    public static ILoggerFactory AddSqlDbCommandLogger(this ILoggerFactory loggerFactory)
    {
        var options = new SqlDbLoggerOptions
        {
            LogLevel = LogLevel.Information
        };
        return loggerFactory.AddSqlDbCommandLogger(options);
    }

    public static ILoggerFactory AddSqlDbCommandLogger(this ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        var options = configuration.Get<SqlDbLoggerOptions>();
        return loggerFactory.AddSqlDbCommandLogger(options);
    }

    public static ILoggerFactory AddSqlDbCommandLogger(this ILoggerFactory loggerFactory, Action<SqlDbLoggerOptions> configure)
    {
        var options = new SqlDbLoggerOptions();
        configure(options);
        return loggerFactory.AddSqlDbCommandLogger(options);
    }
}