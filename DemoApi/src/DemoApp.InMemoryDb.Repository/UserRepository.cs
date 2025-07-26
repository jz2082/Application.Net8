using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using InMemoryDb.Model;
using InMemoryDb.DataEntity;
using Application.Framework;

namespace Demo.InMemoryDb.Repository;

public class UserRepository(InMemoryDbContext context, ILogger<UserRepository> logger) : BaseObject(logger), IUserRepository
{
    private readonly InMemoryDbContext _context = context;

    private static UserModel? EntityToModel(UserEntity? x)
    {
        if (x == null) return null;
        return new UserModel()
        {
            Id = x.Id,
            Name = x.Name,
            Password = x.Password,
            FavoriteColor = x.FavoriteColor,
            Role = x.Role,
            GoogleId = x.GoogleId
        };
    }

    private static UserEntity? ModelToEntity(UserModel? dto)
    {
        if (dto == null) return null;
        return new UserEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Password = dto.Password,
            FavoriteColor = dto.FavoriteColor,
            Role = dto.Role,
            GoogleId = dto.GoogleId
        };
    }

    public async Task<IEnumerable<UserModel>?> GetAllAsync()
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "UserRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"UserRepository.GetAllAsync()");
        info.Add("ReturnType", "IEnumerable<UserModel>");
        IEnumerable<UserModel>? returnValue = [];

        try
        {
            ClearViolation();
            returnValue = await _context
                .Users
                .Select(x => new UserModel { 
                    Id = x.Id, 
                    Name = x.Name, 
                    FavoriteColor = x.FavoriteColor 
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (returnValue == null)
            {
                RuleViolation.Add("returnValue", "Product list not found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<UserModel?> GetAsync(string username)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "UserRepository");
        info.Add("Method", "GetAsync");
        info.Add("Message", $"UserRepository.GetAsync(string username: {username})");
        info.Add("ReturnType", "UserModel?");
        UserModel? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context
                .Users
                .SingleOrDefaultAsync(x => x.Name == username)
                .ConfigureAwait(false);
            returnValue = EntityToModel(entity);
            if (entity == null)
            {
                RuleViolation.Add("returnValue", $"User with username: {username} not found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<UserModel?> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "UserRepository");
        info.Add("Method", "GetByUsernameAndPasswordAsync");
        info.Add("Message", $"UserRepository.GetByUsernameAndPasswordAsync(string username: {username} , string password)");
        info.Add("ReturnType", "UserModel?");
        UserModel? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context
                .Users
                .SingleOrDefaultAsync(x => x.Name == username)
                .ConfigureAwait(false);
            returnValue = EntityToModel(entity);
            if (entity == null)
            {
                RuleViolation.Add("returnValue", $"User with username: {username} not found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }
}
