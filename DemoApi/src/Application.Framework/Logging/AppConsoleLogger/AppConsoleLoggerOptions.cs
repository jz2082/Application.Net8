using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Application.Framework.Logging;

public class AppConsoleLoggerOptions : ConsoleLoggerOptions
{
    public string Environment { get; set; }
    public LogLevel LogLevel { get; set; } = LogLevel.Trace;
    public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
}

