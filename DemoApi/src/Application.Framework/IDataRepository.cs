using System.Runtime.CompilerServices;

namespace Application.Framework;

public interface IDataRepository<T> where T : BaseModel<Guid>
{
    public async Task<IAsyncEnumerable<T>> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<IAsyncEnumerable<T>> GetIAsyncEnumerableListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)"));

    public async Task<bool> FindAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<bool> FindAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)"));

    public async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<long> GetEntityListCountAsync(CancellationToken cancellationToken = default)"));

    public async Task<IEnumerable<T>> GetEntityListAsync(CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<IEnumerable<T>> GetEntityListAsync(CancellationToken cancellationToken = default)"));

    public async Task<PaginatedData<T>> GetPaginatedListAsync(int index, int pageSize, long count = -1, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<PaginatedData<T>> GetPaginatedListAsync(int index, int pageSize, long count = -1, CancellationToken cancellationToken = default)"));

    public async Task<DateTime> GetLatestDateTimeAsync(CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<DateTime> GetLatestDateTimeAsync(CancellationToken cancellationToken = default)"));

    public async Task<long> GetFilteredListCountAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default) =>
       throw await Task.FromResult(new NotImplementedException("public async Task<long> GetFilteredListCountAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)"));

    public async Task<PaginatedData<T>> GetFilteredListAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<PaginatedData<T>> GetFilteredListAsync(SearchCondition searchCondition, CancellationToken cancellationToken = default)"));

    public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)"));

    public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)"));

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)"));

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        throw await Task.FromResult(new NotImplementedException("public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)"));
}
