using InMemoryDb.Model;

namespace Demo.InMemoryDb.Repository;

public interface IBidRepository
{
    Task<IEnumerable<Bid>?> GetAllAsync(int houseId);
    Task<Bid?> AddAsync(Bid model);
}
