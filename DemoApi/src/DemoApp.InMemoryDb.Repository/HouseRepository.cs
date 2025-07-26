using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Application.Framework;
using InMemoryDb.Model;
using InMemoryDb.DataEntity;

namespace Demo.InMemoryDb.Repository;

public class HouseRepository(InMemoryDbContext context, ILogger<HouseRepository> logger) : BaseRepository<House, int>(logger), IHouseRepository
{
    private readonly InMemoryDbContext _context = context;

    private static House? EntityToModel(HouseEntity? x)
    {
        if (x == null) return null;
        return new House
        {
            Id = x.Id,
            Address = x.Address,
            Country = x.Country,
            Description = x.Description,
            Price = x.Price,
            Photo = x.Photo,
            RowVersion=x.RowVersion
        };
    }

    private static HouseEntity? ModelToEntity(House? dto)
    {
        if (dto == null) return null;
        return new HouseEntity
        {
            Address = dto.Address,
            Country = dto.Country,
            Description = dto.Description,
            Price = dto.Price,
            Photo = dto.Photo,
            RowVersion = dto.RowVersion
        };
    }

    private static void ModelToEntity(House? dto, HouseEntity? x)
    {
        if (dto != null && x != null)
        {
            x.Address = dto.Address;
            x.Country = dto.Country;
            x.Description = dto.Description;
            x.Price = dto.Price;
            x.Photo = dto.Photo;
            x.RowVersion = dto.RowVersion;
        }
    }

    public async Task<IEnumerable<House>?> GetAllAsync() 
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "HouseRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"HouseRepository.GetAllAsync()");
        info.Add("ReturnType", "IEnumerable<House>");
        IEnumerable<House> returnValue =[];

        try
        {
            ClearViolation();
            returnValue = await _context
                .Houses
                .Select(x => new House {
                    Id = x.Id,
                    Address = x.Address,
                    Country = x.Country,
                    Price = x.Price
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (returnValue == null)
            {
                RuleViolation.Add("returnValue", "House list not found.");
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

    public async Task<House?> GetAsync(int id)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "HouseRepository");
        info.Add("Method", "GetAsync");
        info.Add("Message", $"HouseRepository.GetAsync(int id: { id })");
        info.Add("ReturnType", "House?");
        House? returnValue = null;

        ClearViolation();
        RuleViolation.Add((await ValidateEntityIdAsync(id).ConfigureAwait(false)).Value);
        if (!HasViolation && !HasException)
        {
            try
            {
                var entity = await _context
                    .Houses
                    .SingleOrDefaultAsync(x => x.Id == id)
                    .ConfigureAwait(false);
                returnValue = EntityToModel(entity);
                if (entity == null)
                {
                    RuleViolation.Add("returnValue", $"House with ID: {id} not found.");
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
                throw;
            }
        }
        return returnValue;
    }

    public async Task<House?> AddAsync(House dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "HouseRepository");
        info.Add("Method", "AddAsync");
        info.Add("Message", $"HouseRepository.AddAsync(House dto)");
        info.Add("ReturnType", "House?");
        House? returnValue;
       
        try
        {
            ClearViolation();
            var entity = (dto == null) ? throw new ArgumentException("Add house with null.") : ModelToEntity(dto);
            if (entity != null)
            {
                _context.Houses.Add(entity);
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

    public async Task<House?> UpdateAsync(House dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "HouseRepository");
        info.Add("Method", "UpdateAsync");
        info.Add("Message", $"HouseRepository.UpdateAsync(House dto)");
        info.Add("ReturnType", "House?");
        House? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context.Houses.FindAsync(dto.Id) ?? throw new ArgumentException(
                $"Trying to update house: entity with ID: {dto.Id} not found."
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
        info.Add(AppLogger.Module, "HouseRepository");
        info.Add("Method", "DeleteAsync");
        info.Add("Message", $"HouseRepository.DeleteAsync(int id)");
        bool returnValue;

        try
        {
            ClearViolation();
            var entity = await _context.Houses.FindAsync(id) ?? throw new ArgumentException(
                $"Trying to delete house: entity with ID: {id} not found."
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
