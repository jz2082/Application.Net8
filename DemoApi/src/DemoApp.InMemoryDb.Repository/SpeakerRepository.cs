using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using InMemoryDb.Model;
using InMemoryDb.DataEntity;
using Application.Framework;

namespace Demo.InMemoryDb.Repository;

public class SpeakerRepository(InMemoryDbContext context, ILogger<SpeakerRepository> logger) : BaseObject(logger), ISpeakerRepository
{
    private readonly InMemoryDbContext _context = context;

    private static Speaker? EntityToModel(SpeakerEntity? x)
    {
        if (x == null) return null;
        return new Speaker
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Sat = x.Sat,
            Sun = x.Sun,
            Favorite = x.Favorite,
            Bio = x.Bio,
            Company = x.Company,
            TwitterHandle = x.TwitterHandle,
            UserBioShort = x.UserBioShort,
            ImageUrl = x.ImageUrl,
            Email = x.Email,
            Photo = x.Photo
        };
    }

    private static SpeakerEntity? ModelToEntity(Speaker? x)
    {
        if (x == null) return null;
        return new SpeakerEntity
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Sat = x.Sat,
            Sun = x.Sun,
            Favorite = x.Favorite,
            Bio = x.Bio,
            Company = x.Company,
            TwitterHandle = x.TwitterHandle,
            UserBioShort = x.UserBioShort,
            ImageUrl = x.ImageUrl,
            Email = x.Email,
            Photo = x.Photo
        };
    }

    private static void ModelToEntity(Speaker dto, SpeakerEntity? x)
    {
        if (dto != null && x != null)
        {
            x.FirstName = dto.FirstName;
            x.LastName = dto.LastName;
            x.Sat = dto.Sat;
            x.Sun = dto.Sun;
            x.Favorite = dto.Favorite;
            x.Bio = dto.Bio;
            x.Company = dto.Company;
            x.TwitterHandle = dto.TwitterHandle;
            x.UserBioShort = dto.UserBioShort;
            x.ImageUrl = dto.ImageUrl;
            x.Email = dto.Email;
            x.Photo = dto.Photo;
        }
    }

    public async Task<IEnumerable<Speaker>?> GetAllAsync()
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "SpeakerRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"SpeakerRepository.GetAllAsync()");
        info.Add("ReturnType", "IEnumerable<Speaker>");
        IEnumerable<Speaker> returnValue =[];

        try
        {
            ClearViolation();
            returnValue = await _context
                .Speakers
                .Select(x => new Speaker
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Sat = x.Sat,
                    Sun = x.Sun,
                    Favorite = x.Favorite,
                    Bio = x.Bio,
                    Company = x.Company,
                    TwitterHandle = x.TwitterHandle,
                    UserBioShort = x.UserBioShort,
                    ImageUrl = x.ImageUrl,
                    Email = x.Email,
                    Photo = x.Photo
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (returnValue == null)
            {
                RuleViolation.Add("returnValue", "Speaker list not found.");
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

    public async Task<Speaker?> GetAsync(int id)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "SpeakerRepository");
        info.Add("Method", "GetAsync");
        info.Add("Message", $"SpeakerRepository.GetAsync(int id)");
        info.Add("ReturnType", "Speaker?");
        Speaker? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context
                .Speakers
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            returnValue = EntityToModel(entity);
            if (entity == null)
            {
                RuleViolation.Add("returnValue", $"Speaker with ID: {id} not found.");
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

    public async Task<Speaker?> AddAsync(Speaker dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "SpeakerRepository");
        info.Add("Method", "AddAsync");
        info.Add("Message", $"SpeakerRepository.AddAsync(Speaker dto)");
        info.Add("ReturnType", "Speaker?");
        Speaker? returnValue;

        try
        {
            ClearViolation();
            var entity = (dto == null) ? throw new ArgumentException("Add speaker with null.") : ModelToEntity(dto);
            if (entity != null)
            {
                _context.Speakers.Add(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            returnValue = EntityToModel(entity);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<Speaker?> UpdateAsync(Speaker dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "SpeakerRepository");
        info.Add("Method", "UpdateAsync");
        info.Add("Message", $"SpeakerRepository.UpdateAsync(Speaker dto)");
        info.Add("ReturnType", "Speaker?");
        Speaker? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context.Speakers.FindAsync(dto.Id) ?? throw new ArgumentException(
                $"Trying to update speaker: entity with ID {dto.Id} not found."
            );
            ModelToEntity(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            returnValue = EntityToModel(entity);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "SpeakerRepository");
        info.Add("Method", "DeleteAsync");
        info.Add("Message", $"SpeakerRepository.DeleteAsync(int id)");
        info.Add("ReturnType", "Speaker?");
        bool returnValue;

        try
        {
            ClearViolation();
            var entity = await _context.Houses.FindAsync(id) ?? throw new ArgumentException(
                $"Trying to delete speaker: entity with ID {id} not found."
            );
            _context.Houses.Remove(entity);
            await _context.SaveChangesAsync();
            returnValue = true;
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
