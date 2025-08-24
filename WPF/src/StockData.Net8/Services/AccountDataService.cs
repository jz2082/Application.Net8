using Microsoft.EntityFrameworkCore;
using StockData.Net8.Services.Common;
using StockService.Net8.Models;
using StockService.Net8.Services;

namespace StockData.Net8.Services;

public class AccountDataService(StockDataDbContext dbContext, NonQueryDataService<Account> nonQueryDataService) : IAccountService
{
    private readonly StockDataDbContext _dbContext = dbContext;
    private readonly NonQueryDataService<Account> _nonQueryDataService = nonQueryDataService;

    public async Task<Account> Create(Account entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<Account> Get(int id)
    {
        Account? entity = await _dbContext.Accounts
            .Include(a => a.AccountHolder)
            .Include(a => a.AssetTransactions)
            .FirstOrDefaultAsync((e) => e.Id == id);
        
        return entity;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        IEnumerable<Account> entities = await _dbContext.Accounts
            .Include(a => a.AccountHolder)
            .Include(a => a.AssetTransactions)
            .ToListAsync();
     
        return entities;
    }

    public async Task<Account> GetByEmail(string email)
    {
        return await _dbContext.Accounts
            .Include(a => a.AccountHolder)
            .Include(a => a.AssetTransactions)
            .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
    }

    public async Task<Account> GetByUsername(string username)
    {
        return await _dbContext.Accounts
            .Include(a => a.AccountHolder)
            .Include(a => a.AssetTransactions)
            .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);
    }

    public async Task<Account> Update(int id, Account entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}