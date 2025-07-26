using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
public class HouseController : BaseController
{
    private readonly IHouseRepository _repository;

    /// <summary>
    ///     Api House Constructor
    /// </summary>
    public HouseController(IHouseRepository repository, ILogger<HouseController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "HouseController");
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
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<House>>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "HouseController.GetAllAsync()");
        var returnValue = new ApiResponse<IEnumerable<House>>();

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
            _logger.LogError(ex, "HouseController.GetAllAsyncfailed.");
        }
        return Ok(HandleApiResponse<IEnumerable<House>>(info, returnValue));
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
    [ProducesResponseType(typeof(ApiResponse<House>), 200)]
    public async Task<IActionResult> GetAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "HouseController.GetAsync(int entityId)");
        var returnValue = new ApiResponse<House>();

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
            _logger.LogError(ex, "HouseController.GetAsync failed.");
        }
        return Ok(HandleApiResponse<House>(info, returnValue));
    }

    /// <summary>
    ///     Add new entity
    /// </summary>
    /// <param name="entity">House</param>
    /// <remarks>
    ///     AddAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("entity")]
    [ProducesResponseType(typeof(House), 200)]
    public async Task<IActionResult> AddAsync([FromBody] House entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "AddAsync");
        info.Add("Message", "HouseController.AddAsync([FromBody] House entity)");
        var returnValue = new ApiResponse<House>();

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
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.AddAsync failed.");
        }
        return Ok(HandleApiResponse<House>(info, returnValue));
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">House</param>
    /// <remarks>
    ///     UpdateAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("entity")]
    [ProducesResponseType(typeof(House), 200)]
    public async Task<IActionResult> UpdateAsync([FromBody] House entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "UpdateAsync");
        info.Add("Message", "HouseController.UpdateAsync([FromBody] House entity)");
        var returnValue = new ApiResponse<House>();

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
            _logger.LogError(ex, "HouseController.UpdateAsync failed.");
        }
        return Ok(HandleApiResponse<House>(info, returnValue));
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
        info.Add("Message", "HouseController.DeleteAsync(int entityId)");
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
            _logger.LogError(ex, "HouseController.DeleteAsync failed.");
        }
        return Ok(HandleApiResponse<bool>(info, returnValue));
    }

}