using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Framework.Logging;

public class LoggerInfoRepository : BaseEntityRepository<LoggerInfoEntity>
{
    private readonly LinqExpressionParser _linqExpressionParser = new();
    private readonly SqlLoggerDbContext _dbContext;

    public LoggerInfoRepository(SqlLoggerDbContext dbContext) : base()
    {
        _info.Add(AppLogger.Application, "Application.Framework.Logging");
        _info.Add(AppLogger.Module, "LoggerInfoRepository");

        _dbContext = dbContext;
    }

    #region Validator 

    protected override void SetupValidatorList()
    {
        InsertValidatorList =
        [
            ValidateNullEntityAsync,
            ValidateUniqueEntityAsync
        ];
        UpdateValidatorList =
        [
            ValidateNullEntityAsync,
            ValidateExistEntityAsync,
            ValidateConcurrencyEntityAsync
        ];
        DeleteValidatorList =
        [
            ValidateNullEntityAsync,
            ValidateExistEntityAsync
        ];
    }

    private async Task<AdditionalInfo> ValidateNullEntityAsync(LoggerInfoEntity entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity == null)
        {
            errInfo.AddError("LoggerInfo", "LoggerInfo can not be null.");
        }
        return await Task.FromResult(errInfo).ConfigureAwait(false);
    }

    private async Task<AdditionalInfo> ValidateUniqueEntityAsync(LoggerInfoEntity entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _dbContext.LoggerInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id)
                .ConfigureAwait(false);
            if (originalEntity != null)
            {
                errInfo.AddError("LoggerInfo", "LoggerInfo already exist and can not be added.");
            }
        }
        return errInfo;
    }

    private async Task<AdditionalInfo> ValidateExistEntityAsync(LoggerInfoEntity entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _dbContext.LoggerInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id)
                .ConfigureAwait(false);
            if (originalEntity == null)
            {
                errInfo.AddError("LoggerInfo", "LoggerInfo is not found and can not be updated/deleted.");
            }
        }
        return errInfo;
    }

    private async Task<AdditionalInfo> ValidateConcurrencyEntityAsync(LoggerInfoEntity entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _dbContext.LoggerInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id)
                .ConfigureAwait(false);
            if (originalEntity != null && !originalEntity.RowVersion.SequenceEqual(entity.RowVersion))
            {
                errInfo.AddError("LoggerInfo", "Data has been modified since entitiy were loaded.");
            }
        }
        return errInfo;
    }

    #endregion

    public override async IAsyncEnumerable<LoggerInfoEntity> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetIAsyncEnumerableListAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "IAsyncEnumerable<LoggerInfo>");

        ClearViolation();
        var entityList = await _dbContext.LoggerInfo
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

    public override async Task<bool> FindAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "FindAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.FindAsync(SearchCondition searchCondition: {searchCondition.GetValue()}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "bool");
        bool returnValue = false;

        try
        {
            ClearViolation();
            if (searchCondition == null || searchCondition.SearchRuleList == null || searchCondition.SearchRuleList.Count == 0)
            {
                return false;
            }
            var predicate = _linqExpressionParser.ParseExpressionOf<LoggerInfoEntity>(searchCondition);
            info.Add("Predicate", _linqExpressionParser.ToString());
            var returnEntity = await _dbContext.LoggerInfo
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(x => x.TimeStamp)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
            returnValue = returnEntity != null;
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "GetEntityListCountAsync");
        info.Add("Message", $"LoggerInfoRepository.GetEntityListCountAsync(CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "long");
        long returnValue = 0;

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfo select l;
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
        info.Add("Message", $"LoggerInfoRepository.GetEntityListAsync(CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "IEnumerable<Product>");
        IEnumerable<LoggerInfoEntity> returnValue = [];

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfo select l;
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
        info.Add("Message", $"LoggerInfoRepository.GetPaginatedListAsync(int index: {index}, int pageSize: {pageSize}, long count: {count}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "PaginatedList<Product>");
        var returnValue = new PaginatedData<LoggerInfoEntity>(index, pageSize, 0, null);

        try
        {
            ClearViolation();
            IQueryable<LoggerInfoEntity> query = from l in _dbContext.LoggerInfo select l;
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

    public override async Task<long> GetFilteredListCountAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetFilteredListCountAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.GetFilteredListCountAsync(SearchCriteria searchCriteria: {searchCriteria.GetValue()}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "long");
        long returnValue = 0;

        try
        {
            ClearViolation();
            if (searchCriteria == null || searchCriteria.SearchRuleList == null || searchCriteria.SearchRuleList?.Count() == 0)
            {
                return returnValue;
            }
            var predicate = _linqExpressionParser.ParseExpressionOf<LoggerInfoEntity>(searchCriteria.ToSearchCondition());
            info.Add("Predicate", _linqExpressionParser.ToString());
            IQueryable<LoggerInfoEntity> query = _dbContext.LoggerInfo;
            query = query.AsNoTracking();
            query = query.Where(predicate);
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

    public override async Task<PaginatedData<LoggerInfoEntity>> GetFilteredListAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "GetFilteredListAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.GetFilteredListAsync(SearchCriteria searchCriteria: {searchCriteria.GetValue()}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "PaginatedData<LoggerInfo>");
        var returnValue = new PaginatedData<LoggerInfoEntity>(searchCriteria.Index, searchCriteria.PageSize, 0, null);

        try
        {
            ClearViolation();
            var predicate = _linqExpressionParser.ParseExpressionOf<LoggerInfoEntity>(searchCriteria.ToSearchCondition());
            info.Add("Predicate", _linqExpressionParser.ToString());
            var filterQuery = _dbContext.LoggerInfo
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(x => x.TimeStamp);
            var totalItems = searchCriteria.Count > 0 ? searchCriteria.Count : await filterQuery.LongCountAsync(cancellationToken).ConfigureAwait(false);
            filterQuery = filterQuery
                .Skip(searchCriteria.Index)
                .Take(searchCriteria.PageSize)
                .OrderByDescending(x => x.TimeStamp);
            var entityList = await filterQuery.ToListAsync(cancellationToken).ConfigureAwait(false);
            returnValue = new PaginatedData<LoggerInfoEntity>(
                searchCriteria.Index,
                searchCriteria.PageSize,
                totalItems,
                entityList);
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
            returnValue = await _dbContext.LoggerInfo
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

    public override async Task<LoggerInfoEntity> InsertAsync(LoggerInfoEntity entity, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "InsertAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.InsertAsync(LoggerInfo entity: {entity.GetValue()}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "LoggerInfo");
        LoggerInfoEntity? returnValue = null;

        try
        {
            ClearViolation();
            RuleViolation.Add((await ValidateInsertAsync(entity).ConfigureAwait(false)).Value);
            if (HasViolation || HasException)
            {
                return returnValue;
            }
            var newEntity = new LoggerInfoEntity();
            newEntity.SetValue(entity);
            returnValue = (await _dbContext.LoggerInfo.AddAsync(newEntity, cancellationToken).ConfigureAwait(false)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<LoggerInfoEntity> UpdateAsync(LoggerInfoEntity entity, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "UpdateAsync");
        info.Add(AppLogger.Message, $"LoggerInfoRepository.UpdateAsync(LoggerInfoEntity entity: {entity.GetValue()}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "LoggerInfoEntity");
        LoggerInfoEntity? returnValue = null;

        try
        {
            ClearViolation();
            RuleViolation.Add((await ValidateUpdateAsync(entity).ConfigureAwait(false)).Value);
            if (HasViolation || HasException)
            {
                return returnValue;
            }
            var originalEntity = await _dbContext.LoggerInfo
                .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken)
                .ConfigureAwait(false);
            if (originalEntity != null && !originalEntity.EntityEquals(entity))
            {
                _dbContext.Entry(originalEntity).Property("RowVersion").OriginalValue = entity.RowVersion;
                originalEntity.SetValue(entity);
                returnValue = (_dbContext.LoggerInfo.Update(originalEntity)).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ExceptionMessage = ex.Message;
            RuleViolation.AddError("WarningMessage", "Data has been modified since entitiy were loaded.");
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public override async Task<bool> DeleteAsync(string entityId, CancellationToken cancellationToken = default)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add("Method", "DeleteAsync");
        info.Add("Message", $"LoggerInfoRepository.DeleteAsync(string entityId: {entityId}, CancellationToken cancellationToken = default)");
        info.Add("ReturnType", "bool");
        bool returnValue = false;

        try
        {
            ClearViolation();
            RuleViolation.Add((await ValidateEntityIdAsync(entityId).ConfigureAwait(false)).Value);
            RuleViolation.Add((await ValidateDeleteAsync(await GetByIdAsync(entityId, cancellationToken).ConfigureAwait(false))).Value);
            if (HasViolation || HasException)
            {
                return returnValue;
            }
            var returnEntity = await _dbContext.LoggerInfo
                .FirstOrDefaultAsync(x => x.Id == GetModelId(entityId), cancellationToken)
                .ConfigureAwait(false);
            _dbContext.LoggerInfo.Remove(returnEntity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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