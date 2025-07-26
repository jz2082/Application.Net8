using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

public abstract class BaseDemoController : Controller, IViolationInfo
{
    protected AdditionalInfo _info = new();
    protected AdditionalInfo _ruleViolation = new();
    protected string _exceptionMessage = string.Empty;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IOptions<AzureOption> _azureOptions;
    protected readonly ILogger<BaseDemoController> _logger;
    protected AdditionalInfo _controllerSetting = new();

    public BaseDemoController(ILogger<BaseDemoController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseDemoController");
        // setup DI
        _logger = logger;
    }

    public BaseDemoController(IHttpContextAccessor httpContextAccessor, ILogger<BaseDemoController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseDemoController");
        // setup DI
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public BaseDemoController(IOptions<AzureOption> azureOptions, IHttpContextAccessor httpContextAccessor, ILogger<BaseDemoController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseDemoController");
        // setup DI
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _azureOptions = azureOptions;
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

    #region Required Controller Implementation

    protected virtual AdditionalInfo ValidateController()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateController");
        info.Add(AppLogger.Message, "protected virtual AdditionalInfo ValidateController()");

        var errInfo = new AdditionalInfo();
        errInfo.AddError("BaseDemoController", "BaseDemoController Setting cannot be null or empty, please correct.");

        try
        {
            if (_controllerSetting == null)
            {
                errInfo.AddError("BaseDemoController", "BaseDemoController Setting cannot be null or empty, please correct.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return errInfo;
    }

    protected virtual async Task ConfigControllerAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ConfigControllerAsync");
        info.Add(AppLogger.Message, "BaseDemoController.ConfigControllerAsync()");

        await Task.FromResult(false).ConfigureAwait(false);
        var exception = new NotImplementedException("BaseDemoController.ConfigControllerAsync() Not Implemented.");
        ExceptionMessage = exception.Message;
        info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, exception);
        throw exception;
    }

    #endregion

    protected void ClearViolation()
    {
        ExceptionMessage = string.Empty;
        RuleViolation.Clear();
        ModelState.Clear();
    }

    protected void ProcessViolationInfo(IViolationInfo violationInfo)
    {
        if (violationInfo != null)
        {
            if (violationInfo.HasViolation)
            {
                RuleViolation.Add(violationInfo.RuleViolation.Value);
            }
            if (violationInfo.HasException)
            {
                ExceptionMessage = violationInfo.ExceptionMessage;
            }
        }
    }

    /// <summary>
    ///     Get current Date Time
    /// </summary>
    /// <remarks>
    ///     This action is used to test WebAPI site is alive.
    ///     <code>
    ///         Return: Result &lt; DateTime &gt;
    ///     </code>
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("Health/DateTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetDateTime()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetDateTime");
        info.Add(AppLogger.Message, "BaseDemoController.GetDateTime()");
        info.Add("ReturnType", "Result");

        try
        {
            ClearViolation();
            var returnValue = await Task.FromResult(DateTime.Now).ConfigureAwait(false);
            return Results.Ok(returnValue);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "BaseDemoController.GetDateTime failed.");
        }
        return Results.Problem("BaseDemoController.GetDateTime failed.", statusCode: 400);
    }
}