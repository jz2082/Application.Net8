namespace Application.Framework.Logging;

public record LoggerInfoEntity : BaseEntity<Guid>
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

    #region Constructor

    /// <summary>
    ///     Constructor
    /// </summary>
    public LoggerInfoEntity()
    { }

    /// <summary>
    ///     Constructor with json string
    /// </summary>
    public LoggerInfoEntity(string jsonString)
    {
        this.SetValue(jsonString);
    }

    /// <summary>
    ///     Constructor with AdditionalInfo string
    /// </summary>
    public LoggerInfoEntity(AdditionalInfo info)
    {
        if (info != null)
        {
            this.Id = Guid.Parse(info.GetStringValue("LoggerId", true));
            this.MachineName = info.GetStringValue("MachineName", true);
            this.LoggerName = info.GetStringValue("LoggerName", true);
            this.Level = info.GetStringValue("Level", true);
            this.ThreadName = info.GetStringValue("ThreadName", true);
            this.TimeStamp = Convert.ToDateTime(info.GetValue("TimeStamp", true));
            this.Application = info.GetStringValue(AppLogger.Application, true);
            this.Module = info.GetStringValue(AppLogger.Module, true);
            this.Method = info.GetStringValue(AppLogger.Method, true);
            this.UserName = info.GetStringValue("UserName", true);
            this.RenderedType = info.GetStringValue("RenderedType", true);
            this.ExecutionTime = Convert.ToDouble(info.GetValue("ExecutionTime", true));
            this.IsJsonMessage = Convert.ToBoolean(info.GetValue("IsJsonMessage", true));
            this.Message = info.GetStringValue(AppLogger.Message, true);
            this.ExceptionMessage = info.GetStringValue("ExceptionMessage", true);
            this.ExceptionType = info.GetStringValue("ExceptionType", true);
            this.ExceptionInfo = info.GetStringValue("ExceptionInfo", true);
            this.DateCreated = Convert.ToDateTime(info.GetValue("DateCreated", true));
            this.DateModified = Convert.ToDateTime(info.GetValue("DateModified", true));
            this.RenderedInfo = this.IsJsonMessage ? info.GetValue() : info.GetStringValue("RenderedInfo", true);
        }
    }

    #endregion
}