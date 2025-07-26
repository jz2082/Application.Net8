using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Application.Framework.Logging;

public class LoggerInfoInMemoryRepository : BaseEntityRepository<LoggerInfoEntity>
{
    private readonly InMemoryLoggerDbContext _dbContext;

    public LoggerInfoInMemoryRepository(InMemoryLoggerDbContext dbContext) : base()
    {
        _info.Add(AppLogger.Application, "Application.Framework.Logging");
        _info.Add(AppLogger.Module, "LoggerInfoInMemoryRepository");

        _dbContext = dbContext;
    }

    public override async IAsyncEnumerable<LoggerInfoEntity> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetIAsyncEnumerableListAsync");
        info.Add(AppLogger.Message, $"LoggerInfoInMemoryRepository.GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "IAsyncEnumerable<LoggerInfo>");

        ClearViolation();
        var entityList = await _dbContext.LoggerInfos
            .AsNoTracking()
            .OrderByDescending(x => x.TimeStamp)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (entityList != null && entityList.Count > 0)
        {
            foreach (var item in entityList)
            {
                yield return item;
            }
        }
        else
        {
            yield return null;
        }
    }

    public override async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "GetEntityListCountAsync");
        info.Add("Message", $"LoggerInfoInMemoryRepository.GetEntityListCountAsync(CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "long");
        long returnValue = 0;

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfos select l;
            query = query.AsNoTracking();
            returnValue = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<IEnumerable<LoggerInfoEntity>> GetEntityListAsync(CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "GetEntityListCountAsync");
        info.Add("Message", $"LoggerInfoInMemoryRepository.GetEntityListAsync(CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "IEnumerable<Product>");
        IEnumerable<LoggerInfoEntity> returnValue = [];

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfos select l;
            query = query.AsNoTracking();
            returnValue = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<PaginatedData<LoggerInfoEntity>> GetPaginatedListAsync(int index, int pageSize, long count = -1, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "GetPaginatedListAsync");
        info.Add("Message", $"LoggerInfoInMemoryRepository.GetPaginatedListAsync(int index: {index}, int pageSize: {pageSize}, long count: {count}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "PaginatedList<Product>");
        var returnValue = new PaginatedData<LoggerInfoEntity>(index, pageSize, 0, null);

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfos select l;
            query = query.OrderByDescending(x => x.TimeStamp);
            query = query.AsNoTracking();
            var totalItems = count > 0 ? count : await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
            query = query.Skip(index);
            query = query.Take(pageSize);
            var entityList = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
            returnValue = new PaginatedData<LoggerInfoEntity>(index, pageSize, totalItems, entityList);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<LoggerInfoEntity> GetByIdAsync(string entityId, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetByIdAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.GetByIdAsync(string entityId: {entityId}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "LoggerInfo");
        LoggerInfoEntity? returnValue = null;

        try
        {
            ClearViolation();
            RuleViolation.Add((await ValidateEntityIdAsync(entityId).ConfigureAwait(false)).Value);
            if (HasViolation || HasException)
            {
                return returnValue;
            }
            returnValue = await _dbContext.LoggerInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == GetModelId(entityId), cancellationToken)
                .ConfigureAwait(false);
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
