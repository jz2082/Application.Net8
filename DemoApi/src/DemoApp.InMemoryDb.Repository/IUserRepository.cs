using InMemoryDb.Model;

namespace Demo.InMemoryDb.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>?> GetAllAsync();
    Task<UserModel?> GetAsync(string username);
    Task<UserModel?> GetByUsernameAndPasswordAsync(string username, string password);
}