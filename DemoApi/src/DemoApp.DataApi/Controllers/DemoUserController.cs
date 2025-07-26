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
public class DemoUserController : BaseDemoController
{
    private readonly IUserRepository _repository;

    /// <summary>
    ///     Api User Constructor
    /// </summary>
    public DemoUserController(IUserRepository repository, ILogger<DemoUserController> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "DemoApp.DataApi.Controllers");
        _info.Add(AppLogger.Module, "DemoUserController");
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
    public async Task<IResult> GetAllAsync()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAllAsync");
        info.Add(AppLogger.Message, "DemoUserController.GetAllAsync()");

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
            _logger.LogError(ex, "DemoUserController.GetAllAsync failed.");
        }
        return Results.Problem("DemoUserController.GetAllAsync failed.", statusCode: 400);
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
    public async Task<IResult> GetAsync(string entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetAsync");
        info.Add(AppLogger.Message, "UserController.GetAsync(int entityId)");

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
            _logger.LogError(ex, "UserController.GetAsync failed.");
        }
        return Results.Problem("DemoUserController.GetAsync failed.", statusCode: 400);
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
    public async Task<IResult> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetByUsernameAndPasswordAsync");
        info.Add(AppLogger.Message, "UserController.GetByUsernameAndPasswordAsync(string username, string password)");

        try
        {
            ClearViolation();
            if (!HasViolation && !HasException)
            {
                var returnValue = await _repository.GetByUsernameAndPasswordAsync(username, password).ConfigureAwait(false);
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
            _logger.LogError(ex, "UserController.GetAsync failed.");
        }
        return Results.Problem("DemoUserController.GetByUsernameAndPasswordAsync failed.", statusCode: 400);
    }

}