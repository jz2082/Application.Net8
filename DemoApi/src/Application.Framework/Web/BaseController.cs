using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

public abstract class BaseController : Controller, IViolationInfo
{
    protected AdditionalInfo _info = new();
    protected AdditionalInfo _ruleViolation = new();
    protected string _exceptionMessage = string.Empty;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IOptions<AzureOption> _azureOptions;
    protected readonly ILogger<BaseController> _logger;
    protected AdditionalInfo _controllerSetting = new();

    public BaseController(ILogger<BaseController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseController");
        // setup DI
        _logger = logger;
    }

    public BaseController(IHttpContextAccessor httpContextAccessor, ILogger<BaseController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseController");
        // setup DI
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public BaseController(IOptions<AzureOption> azureOptions, IHttpContextAccessor httpContextAccessor, ILogger<BaseController> logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseController");
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
        try
        {
            if (_controllerSetting == null)
            {
                errInfo.AddError("BaseController", "BaseController Setting cannot be null or empty, please correct.");
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
        info.Add(AppLogger.Message, "BaseController.ConfigControllerAsync()");

        await Task.FromResult(false).ConfigureAwait(false);
        var exception = new NotImplementedException("BaseController.ConfigControllerAsync() Not Implemented.");
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

    protected ApiResponse<T> HandleApiResponse<T>(AdditionalInfo info, ApiResponse<T>? apiResponse)
    {
        if (apiResponse != null)
        {
            if (HasViolation)
            {
                apiResponse.RuleViolation.Add(RuleViolation.Value);
                info.Add(RuleViolation.Value);
                _logger?.LogWarning(info.GetValue(AdditionalInfoType.TotalMilliseconds));
            }
            if (HasException)
            {
                apiResponse.ExceptionMessage = ExceptionMessage;
                info.AddError("ExceptionMessage", ExceptionMessage);
                _logger?.LogWarning(info.GetValue(AdditionalInfoType.TotalMilliseconds));
            }
        }
        return apiResponse;
    }

    /// <summary>
    ///     Get current Date Time
    /// </summary>
    /// <remarks>
    ///     This action is used to test WebAPI site is alive.
    ///     <code>
    ///         Return: ApiResponse &lt; DateTime &gt;
    ///     </code>
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("Health/DateTime")]
    [ProducesResponseType(typeof(ApiResponse<DateTime>), 200)]
    public async Task<IActionResult> GetDateTime()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetDateTime");
        info.Add(AppLogger.Message, "BaseController.GetDateTime()");
        info.Add("ReturnType", "ApiResponse<DateTime>");
        var returnValue = new ApiResponse<DateTime>();

        try
        {
            ClearViolation();
            returnValue.Data = await Task.FromResult(DateTime.Now).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "AboutController.GetDateTime failed.");
        }
        return Ok(HandleApiResponse<DateTime>(info, returnValue));
    }
}