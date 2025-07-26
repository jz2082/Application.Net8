using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using InMemoryDb.Model;
using InMemoryDb.DataEntity;
using Application.Framework;

namespace Demo.InMemoryDb.Repository;

public class BidRepository(InMemoryDbContext context, ILogger<BidRepository> logger) : BaseObject(logger), IBidRepository
{
    private readonly InMemoryDbContext _context = context;

    private static Bid? EntityToModel(BidEntity? x)
    {
        if (x == null) return null;
        return new Bid
        {
            Id = x.Id,
            HouseId = x.HouseId,
            Bidder = x.Bidder,
            Amount = x.Amount,
        };
    }

    private static BidEntity? ModelToEntity(Bid? dto)
    {
        if (dto == null) return null;
        return new BidEntity
        {
            HouseId = dto.HouseId,
            Bidder = dto.Bidder,
            Amount = dto.Amount
        };
    }

    private static void ModelToEntity(Bid? dto, BidEntity? x)
    {
        if (dto != null && x != null)
        {
            x.HouseId = dto.HouseId;
            x.Bidder = dto.Bidder;
            x.Amount = dto.Amount;
        }
    }

    public async Task<IEnumerable<Bid>?> GetAllAsync(int houseId)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "BidRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"BidRepository.GetAllAsync()");
        info.Add("ReturnType", "IEnumerable<Bid>");
        IEnumerable<Bid> returnValue = [];

        try
        {
            ClearViolation();
            returnValue =  await _context 
                .Bids
                .Where(x => x.HouseId == houseId)
                .Select(x => new Bid {
                    Id = x.Id,
                    HouseId = x.HouseId,
                    Bidder = x.Bidder,
                    Amount = x.Amount
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (returnValue == null)
            {
                RuleViolation.Add("returnValue", "Bid list not found.");
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

    public async Task<Bid?> AddAsync(Bid dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "BidRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"BidRepository.AddAsync(Bid dto)");
        info.Add("ReturnType", "Bid?");
        Bid? returnValue;

        try
        {
            ClearViolation();
            var entity = (dto == null) ? throw new ArgumentException("Add bid with null.") : ModelToEntity(dto);
            if (entity != null)
            {
                _context.Bids.Add(entity);
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
}