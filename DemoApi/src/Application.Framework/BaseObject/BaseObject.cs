using Microsoft.Extensions.Logging;

namespace Application.Framework;

public abstract class BaseObject : IViolationInfo, IDisposable
{
    protected AdditionalInfo _info = new();
    protected AdditionalInfo _ruleViolation = new();
    protected string _exceptionMessage = string.Empty;
    protected AdditionalInfo _setting = new();
    protected readonly ILogger<BaseObject> _logger = null;

    public BaseObject()
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseObject");
    }

    public BaseObject(ILogger<BaseObject> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseObject");
        _logger = logger;
    }

    #region IViolationInfo Support

    public string ExceptionMessage
    {
        get { return _exceptionMessage; }
        set { _exceptionMessage = value; }
    }

    public AdditionalInfo RuleViolation
    {
        get { return _ruleViolation; }
    }

    public bool HasViolation => RuleViolation != null && RuleViolation.Value.Count > 0;
    public bool HasException => !string.IsNullOrEmpty(ExceptionMessage);

    #endregion

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls      

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            { }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    protected void ClearViolation()
    {
        ExceptionMessage = string.Empty;
        RuleViolation.Clear();
    }

    protected void ProcessViolationInfo(IViolationInfo violationInfo)
    {
        if (violationInfo != null && violationInfo.HasViolation)
        {
            RuleViolation.Add(violationInfo.RuleViolation.Value);
        }
    }

    public virtual void ApplySetting(AdditionalInfo setting) =>
        throw new NotImplementedException("public override void ApplySetting(AdditionalInfo setting)");

    public static DateTime GetDateTime() => DateTime.Now;

    public static async Task<DateTime> GetDateTimeAsync() => await Task.FromResult(DateTime.Now).ConfigureAwait(false);
 
}