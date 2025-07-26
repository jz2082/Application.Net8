using InMemoryDb.Model;

namespace Demo.InMemoryDb.Repository;

public interface ISpeakerRepository
{
    Task<IEnumerable<Speaker>?> GetAllAsync();
    Task<Speaker?> GetAsync(int id);
    Task<Speaker?> AddAsync(Speaker model);
    Task<Speaker?> UpdateAsync(Speaker model);
    Task<bool> DeleteAsync(int id);
}
