using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Application.Framework;

public abstract class BaseRepository<T, S> : BaseObject where T : BaseModel<S> where S : struct
{
    public BaseRepository() : base()
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseRepository");
        SetupValidatorList();
    }

    public BaseRepository(ILogger<BaseRepository<T, S>> logger) : base(logger)
    {
        _info.Add(AppLogger.Application, "Application.Framework");
        _info.Add(AppLogger.Module, "BaseRepository<T, S>");
        SetupValidatorList();
    }

    #region Validator 

    protected List<Func<T, Task<AdditionalInfo>>> InsertValidatorList { get; set; }
    protected List<Func<T, Task<AdditionalInfo>>> UpdateValidatorList { get; set; }
    protected List<Func<T, Task<AdditionalInfo>>> DeleteValidatorList { get; set; }

    protected virtual void SetupValidatorList()
    {
        InsertValidatorList = [];
        UpdateValidatorList = [];
        DeleteValidatorList = [];
    }

    protected async Task<AdditionalInfo> ValidateInsertAsync(T entityModel)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateInsertAsync");
        info.Add(AppLogger.Message, "public async Task<AdditionalInfo> ValidateInsertAsync(T entityModel)");

        var errInfo = new AdditionalInfo();
        try
        {
            if (InsertValidatorList != null)
            {
                foreach (var func in InsertValidatorList)
                {
                    info.Add("ValidateMethod", func.Method.Name);
                    errInfo.Add((await func(entityModel)).Value);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return errInfo;
    }

    protected async Task<AdditionalInfo> ValidateUpdateAsync(T entityModel)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateUpdateAsync");
        info.Add(AppLogger.Message, "public async Task<AdditionalInfo> ValidateUpdateAsync(T entityModel)");

        var errInfo = new AdditionalInfo();
        try
        {
            if (UpdateValidatorList != null)
            {
                foreach (var func in UpdateValidatorList)
                {
                    info.Add("ValidateMethod", func.Method.Name);
                    errInfo.Add((await func(entityModel)).Value);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return errInfo;
    }

    protected async Task<AdditionalInfo> ValidateDeleteAsync(T entityModel)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateDeleteAsync");
        info.Add(AppLogger.Message, "public async Task<AdditionalInfo> ValidateDeleteAsync(T entityModel)");

        var errInfo = new AdditionalInfo();
        try
        {
            if (DeleteValidatorList != null)
            {
                foreach (var func in DeleteValidatorList)
                {
                    info.Add("ValidateMethod", func.Method.Name);
                    errInfo.Add((await func(entityModel)).Value);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return errInfo;
    }

    protected async Task<AdditionalInfo> ValidateEntityIdAsync(string entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateEntityIdAsync");
        info.Add(AppLogger.Message, "private async Task<AdditionalInfo> ValidateEntityIdAsync(string entityId)");

        var errInfo = new AdditionalInfo();
        try
        {
            if (string.IsNullOrEmpty(entityId))
            {
                errInfo.AddError("Entity", "entityId cannot be null or empty, please correct.");
            }
            // entityId is Guid
            if (!Guid.TryParse(entityId, out Guid id))
            {
                errInfo.AddError("Entity", "entityId is not GUID, please correct.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return await Task.FromResult(errInfo);
    }

    protected async Task<AdditionalInfo> ValidateEntityIdAsync(int entityId)
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "ValidateEntityIdAsync");
        info.Add(AppLogger.Message, "private async Task<AdditionalInfo> ValidateEntityIdAsync(string entityId)");

        var errInfo = new AdditionalInfo();
        try
        {
            if (entityId <= 0)
            {
                errInfo.AddError("Entity", "entityId must great than 0, please correct.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return await Task.FromResult(errInfo);
    }

    protected virtual AdditionalInfo ValidateRepository() =>
        throw new NotImplementedException("protected override AdditionalInfo ValidateRepository()");

    protected virtual async Task ConfigRepositoryAsync(CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("protected override async Task ConfigRepositoryAsync(CancellationToken cancellationToken = default)"));

    #endregion

    protected Guid GetGuidId(string modelId)
    {
        if (typeof(S) == typeof(System.Guid))
        {
            if (Guid.TryParse(modelId, out Guid id))
            {
                return id;
            }
        }
        return Guid.Empty;
    }

    public virtual async IAsyncEnumerable<T> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        yield break;
        throw await Task.FromResult(new NotImplementedException("public virtual async IAsyncEnumerable<T> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<bool> FindAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<bool> FindAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<IEnumerable<T>> GetEntityListAsync(CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<IEnumerable<T>> GetEntityListAsync(CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<PaginatedData<T>> GetPaginatedListAsync(int index, int pageSize, long count = -1, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<PaginatedData<T>> GetPaginatedListAsync(int index, int pageSize, long count = -1, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<DateTime> GetLatestDateTimeAsync(CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<DateTime> GetLatestDateTimeAsync(CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<long> GetFilteredListCountAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<long> GetFilteredListCountAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<PaginatedData<T>> GetFilteredListAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<PaginatedData<T>> GetFilteredListAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<T> GetByIdAsync(string entityId, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<T> GetByIdAsync(string entityId, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<T> GetNewEntityAsync(CancellationToken cancellationToken = default, params string[] stringValues)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<T> GetNewEntityAsync(CancellationToken cancellationToken = default, params string[] stringValues)"));
    }

    public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)"));
    }

    public virtual async Task<bool> DeleteAsync(string entityId, CancellationToken cancellationToken = default)
    {
        throw await Task.FromResult(new NotImplementedException("public virtual async Task<bool> DeleteAsync(string entityId, CancellationToken cancellationToken = default)"));
    }

}