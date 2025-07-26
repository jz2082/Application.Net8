using Microsoft.AspNetCore.Mvc;

using Application.Framework;
using InMemoryDb.Model;
using Demo.InMemoryDb.Repository;

namespace DemoApp.DataApi.Controllers;

/// <summary>
///     DataApi: User
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserRepository _repository;

    /// <summary>
    ///     Api User Constructor
    /// </summary>
    public UserController(IUserRepository repository, ILogger<UserController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "UserController");
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
    [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "UserController.GetAllAsync()");
        var returnValue = new ApiResponse<IEnumerable<UserModel>>();

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
            _logger.LogError(ex, "UserController.GetAllAsync failed.");
        }
        return Ok(HandleApiResponse<IEnumerable<UserModel>>(info, returnValue));
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
    [HttpGet("entity/{entityId}")]
    [ProducesResponseType(typeof(UserModel), 200)]
    public async Task<IActionResult> GetAsync(string entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "UserController.GetAsync(int entityId)");
        var returnValue = new ApiResponse<UserModel>();

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
            _logger.LogError(ex, "UserController.GetAsync failed.");
        }
        return Ok(HandleApiResponse<UserModel>(info, returnValue));
    }

    /// <summary>
    ///     Get entity
    /// </summary>
    /// <param name="username">int</param>
    /// <param name="password">int</param>
    /// <remarks>
    ///     GetByUsernameAndPasswordAsync 
    /// </remarks>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("entity/{username}/{password}")]
    [ProducesResponseType(typeof(UserModel), 200)]
    public async Task<IActionResult> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetByUsernameAndPasswordAsync");
        info.Add(AppLogger.Message, "UserController.GetByUsernameAndPasswordAsync(string username, string password)");
        var returnValue = new ApiResponse<UserModel>();

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                returnValue.Data = await _repository.GetByUsernameAndPasswordAsync(username, password).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger.LogError(ex, "UserController.GetAsync failed.");
        }
        return Ok(HandleApiResponse<UserModel>(info, returnValue));
    }

}