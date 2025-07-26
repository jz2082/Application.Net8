using Microsoft.AspNetCore.Mvc;

using Application.Framework;
using InMemoryDb.Model;
using Demo.InMemoryDb.Repository;

namespace DemoApp.DataApi.Controllers;

/// <summary>
///     DataApi: House
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class SpeakerController : BaseController
{
    private readonly ISpeakerRepository _repository;

    /// <summary>
    ///     Api House Constructor
    /// </summary>
    public SpeakerController(ISpeakerRepository repository, ILogger<SpeakerController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "SpeakerController");
        // setup DI
        _repository = repository;
    }

    /// <summary>
    ///     Get list of entity
    /// </summary>
    /// <remarks>
    ///     GetAllAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("list")]
    [ProducesResponseType(typeof(IEnumerable<Speaker>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "SpeakerController.GetAllAsync()");
        var returnValue = new ApiResponse<IEnumerable<Speaker>>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetAllAsync().ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "SpeakerController.GetAllAsync failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<Speaker>>(info, returnValue));
    }

    /// <summary>
    ///     Get entity
    /// </summary>
    /// <param name="entityId">int</param>
    /// <remarks>
    ///     GetAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("entity/{entityId:int}")]
    [ProducesResponseType(typeof(Speaker), 200)]
    public async Task<IActionResult> GetAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "SpeakerController.GetAsync(int entityId)");
        var returnValue = new ApiResponse<Speaker>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetAsync(entityId).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "SpeakerController.GetAsync failed.");
        }
        return Ok(HandleApiResponse<Speaker>(info, returnValue));
    }

    /// <summary>
    ///     Add new entity
    /// </summary>
    /// <param name="entity">Speaker</param>
    /// <remarks>
    ///     AddAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("entity")]
    [ProducesResponseType(typeof(Speaker), 200)]
    public async Task<IActionResult> AddAsync([FromBody] Speaker entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "AddAsync");
        info.Add("Message", "SpeakerController.AddAsync([FromBody] Speaker entity)");
        var returnValue = new ApiResponse<Speaker>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.AddAsync(entity).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            returnValue.ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "SpeakerController.AddAsync failed.");
        }
        return Ok(HandleApiResponse<Speaker>(info, returnValue));
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">Speaker</param>
    /// <remarks>
    ///     UpdateAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("entity")]
    [ProducesResponseType(typeof(Speaker), 200)]
    public async Task<IActionResult> UpdateAsync([FromBody] Speaker entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "UpdateAsync");
        info.Add("Message", "SpeakerController.UpdateAsync([FromBody] Speaker entity)");
        var returnValue = new ApiResponse<Speaker>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "SpeakerController.UpdateAsync failed.");
        }
        return Ok(HandleApiResponse<Speaker>(info, returnValue));
    }

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entityId">int</param>
    /// <remarks>
    ///     DeleteAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("entity/{entityId:int}")]
    [ProducesResponseType(typeof(Bid), 200)]
    public async Task<IActionResult> DeleteAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "DeleteAsync");
        info.Add("Message", "SpeakerController.DeleteAsync(int entityId)");
        var returnValue = new ApiResponse<bool>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                await _repository.DeleteAsync(entityId).ConfigureAwait(false);
                returnValue.Data = true;
            }
        }
        catch (Exception ex)
        {
            returnValue.Data = false;
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "SpeakerController.DeleteAsync failed.");
        }
        return Ok(HandleApiResponse<bool>(info, returnValue));
    }

}