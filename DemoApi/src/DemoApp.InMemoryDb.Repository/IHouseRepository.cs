using InMemoryDb.Model;

namespace Demo.InMemoryDb.Repository;

public interface IHouseRepository
{
    Task<IEnumerable<House>?> GetAllAsync();
    Task<House?> GetAsync(int id);
    Task<House?> AddAsync(House model);
    Task<House?> UpdateAsync(House model);
    Task<bool> DeleteAsync(int id);
}
