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
public class ProductController : BaseController
{
    private readonly IProductRepository _repository;

    /// <summary>
    ///     Api Product Constructor
    /// </summary>
    public ProductController(IProductRepository repository, ILogger<ProductController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "ProductController");
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
    [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "ProductController.GetAllAsync()");
        var returnValue = new ApiResponse<IEnumerable<Product>>();

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
            _logger.LogError(ex, "ProductController.GetAllAsync failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<Product>>(info, returnValue));
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
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> GetAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "ProductController.GetAsync(int entityId)");
        var returnValue = new ApiResponse<Product>();

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
            _logger.LogError(ex, "ProductController.GetAsync failed.");
        }
        return Ok(HandleApiResponse<Product>(info, returnValue));
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
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> AddAsync([FromBody] Product entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "AddAsync");
        info.Add("Message", "ProductController.AddAsync([FromBody] Product entity)");
        var returnValue = new ApiResponse<Product>();

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
            _logger.LogError(ex, "ProductController.AddAsync failed.");
        }
        return Ok(HandleApiResponse<Product>(info, returnValue)); 
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
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> UpdateAsync([FromBody] Product entity)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "UpdateAsync");
        info.Add("Message", "ProductController.UpdateAsync([FromBody] Product entity)");
        var returnValue = new ApiResponse<Product>();

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
            _logger.LogError(ex, "ProductController.UpdateAsync failed.");
        }
        return Ok(HandleApiResponse<Product>(info, returnValue));
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
        info.Add("Message", "ProductController.DeleteAsync(int entityId)");
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
            _logger.LogError(ex, "ProductController.DeleteAsync failed.");
        }
        return Ok(HandleApiResponse<bool>(info, returnValue));
    }

}