namespace Application.Framework.Logging;

public record LoggerInfoEntity 
{
    public string Environment { get; set; }
    public string MachineName { get; init; }
    public string LoggerName { get; init; }
    public string Level { get; init; }
    public string ThreadName { get; init; }
    public DateTime? TimeStamp { get; init; }
    public string Application { get; init; }
    public string Module { get; init; }
    public string Method { get; init; }
    public string UserName { get; init; }
    public double ExecutionTime { get; init; }
    public bool IsJsonMessage { get; init; }
    public string Message { get; init; }
    public string RenderedType { get; init; }
    public string RenderedInfo { get; init; }
    public string ExceptionMessage { get; init; }
    public string ExceptionType { get; init; }
    public string ExceptionInfo { get; init; }
    public bool Flag { get; set; }
}