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
public class DemoHouseController : BaseDemoController
{
    private readonly IHouseRepository _repository;

    /// <summary>
    ///     Api House Constructor
    /// </summary>
    public DemoHouseController(IHouseRepository repository, ILogger<DemoHouseController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "DemoHouseController");
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
    [ProducesResponseType(typeof(IEnumerable<House>), 200)]
    public async Task<IResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "HouseController.GetAllAsync()");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                var returnValue = await _repository.GetAllAsync().ConfigureAwait(false);
                ProcessViolationInfo((IViolationInfo)_repository);
                if (!HasViolation && !HasException)
                {
                    return Results.Ok(returnValue);
                }
            }
            return Results.ValidationProblem(RuleViolation.ValidationProblem);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.GetAllAsyncfailed.");
        }
        return Results.Problem("DemoHouseController.GetAllAsync failed.", statusCode: 400);
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
    [ProducesResponseType(typeof(House), 200)]
    public async Task<IResult> GetAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "HouseController.GetAsync(int entityId)");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                var returnValue = await _repository.GetAsync(entityId).ConfigureAwait(false);
                ProcessViolationInfo((IViolationInfo)_repository);
                if (!HasViolation && !HasException)
                {
                    return Results.Ok(returnValue);
                }
            }
            return Results.ValidationProblem(RuleViolation.ValidationProblem);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.GetAsync failed.");
        }
        return Results.Problem($"DemoHouseController.GetAsync(int entityId: {entityId}) failed.", statusCode: 400);
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
    public async Task<IResult> AddAsync([FromBody] House entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "AddAsync");
        info.Add("Message", "HouseController.AddAsync([FromBody] House entity)");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                var returnValue = await _repository.AddAsync(entity).ConfigureAwait(false);
                ProcessViolationInfo((IViolationInfo)_repository);
                if (!HasViolation && !HasException)
                {
                    return Results.Ok(returnValue);
                }
            }
            return Results.ValidationProblem(RuleViolation.ValidationProblem);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.AddAsync failed.");
        }
        return Results.Problem("DemoHouseController.AddAsync failed.", statusCode: 400);
    }

    /// <summary>
    ///    Update entity
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
    public async Task<IResult> UpdateAsync([FromBody] House entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "UpdateAsync");
        info.Add("Message", "HouseController.UpdateAsync([FromBody] House entity)");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                var returnValue = await _repository.UpdateAsync(entity).ConfigureAwait(false);
                ProcessViolationInfo((IViolationInfo)_repository);
                if (!HasViolation && !HasException)
                {
                    return Results.Ok(returnValue);
                }
            }
            return Results.ValidationProblem(RuleViolation.ValidationProblem);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.UpdateAsync failed.");
        }
        return Results.Problem("DemoHouseController.UpdateAsync failed.", statusCode: 400);
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
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IResult> DeleteAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "DeleteAsync");
        info.Add("Message", "HouseController.DeleteAsync(int entityId)");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                await _repository.DeleteAsync(entityId).ConfigureAwait(false);
                ProcessViolationInfo((IViolationInfo)_repository);
                if (!HasViolation && !HasException)
                {
                    return Results.Ok(true);
                }
            }
            return Results.ValidationProblem(RuleViolation.ValidationProblem);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "HouseController.DeleteAsync failed.");
        }
        return Results.Problem("DemoHouseController.DeleteAsync failed.", statusCode: 400);
    }

}