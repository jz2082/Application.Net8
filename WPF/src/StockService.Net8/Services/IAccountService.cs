using StockService.Net8.Models;

namespace StockService.Net8.Services;

public interface IAccountService : IDataService<Account>
{
    Task<Account> GetByUsername(string username);
    Task<Account> GetByEmail(string email);
}
