using Microsoft.AspNetCore.Mvc;

using Application.Framework;
using Application.Framework.Logging;

namespace DemoApp.DataApi.Controllers;

/// <summary>
///     DataApi: LoggerInfo
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class LoggerInfoInMemoryController : BaseController
{
    private readonly BaseEntityRepository<LoggerInfoEntity> _repository;

    /// <summary>
    ///     Api House Constructor
    /// </summary>
    public LoggerInfoInMemoryController(BaseEntityRepository<LoggerInfoEntity> repository, ILogger<LoggerInfoInMemoryController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "LoggerInfoInMemoryController");

        // setup DI
        _repository = repository;
    }

    #region Required Controller Implementation

    /// <summary>
    ///     Validate Controller Setting
    /// </summary>
    protected override AdditionalInfo ValidateController()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateController");
        info.Add(AppLogger.Message, "LoggerInfoInMemoryController.ValidateController()");

        var errInfo = new AdditionalInfo();
        try
        {
            // to be Implemented;
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
        info.Add(AppLogger.Message, "LoggerInfoInMemoryController.ConfigControllerAsync()");

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
    ///     Get IEnumerable LoggerInfo
    /// </summary>
    /// <remarks>
    ///     GetAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("list")]
    [ProducesResponseType(typeof(IEnumerable<LoggerInfoEntity>), 200)]
    public async Task<IActionResult> GetEntityListAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetEntityListAsync");
        info.Add(AppLogger.Message, $"LoggerInfoInMemoryController.GetEntityListAsync()");
        info.Add("ReturnType", "IEnumerable<LoggerInfoEntity>");
        var returnValue = new ApiResponse<IEnumerable<LoggerInfoEntity>>();

        try
        {
            ClearViolation();
            RuleViolation.Add(ValidateController().Value);
            await ConfigControllerAsync().ConfigureAwait(false);
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetEntityListAsync().ConfigureAwait(false);
                ProcessViolationInfo(_repository);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "LoggerInfoInMemoryController.GetEntityListAsync failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<LoggerInfoEntity>>(info, returnValue));
    }

    /// <summary>
    ///     Get LoggerInfo
    /// </summary>
    /// <param name="entityId">string</param>
    /// <remarks>
    ///     GetAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("entity/{entityId}")]
    [ProducesResponseType(typeof(LoggerInfoEntity), 200)]
    public async Task<IActionResult> GetEntityAsync(string entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "GetEntityAsync");
        info.Add("Message", $"LoggerInfoInMemoryController.GetEntityAsync(string entityId: {entityId})");
        info.Add("ReturnType", "LoggerInfo");
        var returnValue = new ApiResponse<LoggerInfoEntity>();

        try
        {
            ClearViolation();
            RuleViolation.Add(ValidateController().Value);
            await ConfigControllerAsync().ConfigureAwait(false);
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetByIdAsync(entityId).ConfigureAwait(false);
                ProcessViolationInfo(_repository);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "LoggerInfoInMemoryController.GetEntityAsync failed.");
        }
        return Ok(HandleApiResponse<LoggerInfoEntity>(info, returnValue));
    }

}