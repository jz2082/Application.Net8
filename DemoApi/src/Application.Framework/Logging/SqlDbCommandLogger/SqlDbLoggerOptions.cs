using Microsoft.Extensions.Logging;

namespace Application.Framework.Logging;

public class SqlDbLoggerOptions
{
    public string Environment { get; init; }
    public int NumberOfTries { get; init; }
    public int DeltaTime { get; init; }
    public int MaxTimeInterval { get; init; }
    public LogLevel LogLevel { get; init; } = LogLevel.Trace;
    public string ConnectionString { get; init; }
}

