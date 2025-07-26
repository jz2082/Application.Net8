using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

using Application.Framework;

namespace DemoApp.DataApi.Controllers;

/// <summary>
///     WebApi: About
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class AboutController : BaseController
{
    protected readonly IHealthCheckRepository _healthCheckRepository;

    /// <summary>
    ///     Micro Service: About Constructor
    /// </summary>
    public AboutController(IOptions<AppSetting> appSetting, IHealthCheckRepository healthCheckRepository, ILogger<AboutController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "AboutController");
        // setup DI
        _controllerSetting = new AdditionalInfo(appSetting.Value.GetValue());
        _healthCheckRepository = healthCheckRepository;
    }

    #region Required Controller Implementation

    /// <summary>
    ///     Validate Controller Setting
    /// </summary>
    protected override AdditionalInfo ValidateController()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateController");
        info.Add(AppLogger.Message, "AboutController.ValidateController()");

        var errInfo = new AdditionalInfo();
        try
        {
            if (_controllerSetting == null)
            {
                errInfo.AddError("AboutController", "AboutController Setting cannot be null or empty, please correct.");
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

    /// <summary>
    ///     Config Controller Setting
    /// </summary>
    protected override async Task ConfigControllerAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ConfigControllerAsync");
        info.Add(AppLogger.Message, "AboutController.ConfigControllerAsync()");

        try
        {
            await Task.FromResult(false);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
    }

    #endregion

    /// <summary>
    ///     Get AppSetting
    /// </summary>
    /// <remarks>
    ///     Get AppSetting
    ///     <code>
    ///         Return: ApiResponse &lt; AppSetting &gt;
    ///     </code>
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet()]
    [ProducesResponseType(typeof(ApiResponse<AppSetting>), 200)]
    public async Task<IActionResult> GetAppSetting()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAppSetting");
        info.Add(AppLogger.Message, "AboutController.GetAppSetting()");
        var returnValue = new ApiResponse<AppSetting>();

        try
        {
            ClearViolation();
            RuleViolation.Add(ValidateController().Value);
            await ConfigControllerAsync().ConfigureAwait(false);
            if (!HasViolation && !HasException)
            {
                var dbConnection = _controllerSetting.GetStringValue("DbConnection");
                if (dbConnection.Contains("Password"))
                {
                    dbConnection = dbConnection.Replace("sqldatadbserver", "XXXXXXXX").Replace("Dudu1120", "XXXXXXXX").Replace("sa", "XXXXXXXX").Replace("dbAdmin", "XXXXXXXX");
                }
                returnValue.Data = new AppSetting()
                {
                    Environment = _controllerSetting.GetStringValue("Environment"),
                    DbConnection = dbConnection,
                    DatabaseName = "XXXXXXXX",
                    DownStreamApiUrl = _controllerSetting.GetStringValue("DownStreamApiUrl").Replace("auroraappserver.tplinkdns.com", "www.xxxxxxx.com"),
                    CertificateFileName = _controllerSetting.GetStringValue("CertificateFileName"),
                    CertificatePassword = "XXXXXXXX",
                    AboutMessage = _controllerSetting.GetStringValue("AboutMessage")
                };
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "AboutController.GetAppSetting failed.");
        }
        return Ok(HandleApiResponse<AppSetting>(info, returnValue));
    }

    /// <summary>
    ///     Get Health Check Result
    /// </summary>
    /// <remarks>
    ///     This action is used to test Web API site is alive.
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [AllowAnonymous]
    [HttpGet("Health/Check")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<HealthCheckResult>>), 200)]
    public async Task<IActionResult> HealthCheck()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "HealthCheck");
        info.Add(AppLogger.Message, "AboutController.HealthCheck()");
        info.Add("ReturnType", "ApiResponse<IEnumerable<HealthCheckResult>>");
        var returnValue = new ApiResponse<IEnumerable<HealthCheckResult>>();

        try
        {
            ClearViolation();
            RuleViolation.Add(ValidateController().Value);
            await ConfigControllerAsync().ConfigureAwait(false);
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _healthCheckRepository.HealthCheckAsync().ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "AboutController.HealthCheck failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<HealthCheckResult>>(info, returnValue));
    }

    /// <summary>
    ///     Test exception log and handle
    /// </summary>
    /// <remarks>
    ///     This action is used to test WebAPI exception handle.
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("ApiException")]
    [ProducesResponseType(typeof(ApiResponse<AppSetting>), 200)]
    public async Task<IActionResult> TestApiException()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "TestApiException");
        info.Add(AppLogger.Message, "AboutController.TestApiException()");
        var returnValue = new ApiResponse<AppSetting>();

        try
        {
            ClearViolation();
            RuleViolation.Add(ValidateController().Value);
            await ConfigControllerAsync().ConfigureAwait(false);
            if (!HasViolation && !HasException)
            {
                returnValue.Data = new AppSetting()
                {
                    AboutMessage = _controllerSetting.GetStringValue("AboutMessage")
                };
                int a = 0;
                int b = 10 / a;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "AboutController.TestApiException failed.");
        }
        return Ok(HandleApiResponse<AppSetting>(info, returnValue));
    }
}
