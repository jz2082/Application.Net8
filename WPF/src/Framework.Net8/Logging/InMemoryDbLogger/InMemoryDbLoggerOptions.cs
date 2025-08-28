using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class InMemoryDbLoggerOptions
{
    public string Environment { get; init; }
    public LogLevel LogLevel { get; init; } = LogLevel.Trace;
    public string ConnectionString { get; init; }
}