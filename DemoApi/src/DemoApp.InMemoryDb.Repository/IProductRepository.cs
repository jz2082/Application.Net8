using InMemoryDb.Model;

namespace Demo.InMemoryDb.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>?> GetAllAsync();
    Task<Product?> GetAsync(int productId);
    Task<Product?> AddAsync(Product model);
    Task<Product?> UpdateAsync(Product model);
    Task<bool> DeleteAsync(int productId);
}
