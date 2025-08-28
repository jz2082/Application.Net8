using Microsoft.Extensions.Logging;

namespace Framework.Net8.Logging;

public class InMemoryDbLoggerOptions
{
    public string Environment { get; init; }
    public LogLevel LogLevel { get; init; } = LogLevel.Trace;
    public string ConnectionString { get; init; }
}