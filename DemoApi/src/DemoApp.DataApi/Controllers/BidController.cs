using Microsoft.AspNetCore.Mvc;

using Application.Framework;
using InMemoryDb.Model;
using Demo.InMemoryDb.Repository;

namespace DemoApp.DataApi.Controllers;

/// <summary>
///     DataApi: Bid
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class BidController : BaseController
{
    private readonly IBidRepository _repository;

    /// <summary>
    ///     Api Bid Constructor
    /// </summary>
    public BidController(IBidRepository repository, ILogger<BidController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "BidController");
        // setup DI
        _repository = repository;
    }

    /// <summary>
    ///     Get list of entity
    /// </summary>
    /// <param name="entityId">int</param>
    /// <remarks>
    ///     GetAllAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("list/{entityId:int}/bids")]
    [ProducesResponseType(typeof(IEnumerable<Bid>), 200)]
    public async Task<IActionResult> GetAllAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "BidController.GetAllAsync(int entityId)");
        var returnValue = new ApiResponse<IEnumerable<Bid>>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetAllAsync(entityId).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "BidController.GetAllAsync failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<Bid>>(info, returnValue));
    }

    /// <summary>
    ///     Add new entity
    /// </summary>
    /// <param name="entity">Bid</param>
    /// <remarks>
    ///     PostAsync
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("entity")]
    [ProducesResponseType(typeof(Bid), 200)]
    public async Task<IActionResult> AddAsync([FromBody] Bid entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "AddAsync");
        info.Add("Message", "BidController.AddAsync([FromBody] Bid entity)");
        var returnValue = new ApiResponse<Bid>();

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
            _logger.LogError(ex, "BidController.AddAsync failed.");
        }
        return Ok(HandleApiResponse<Bid>(info, returnValue));
    }

}