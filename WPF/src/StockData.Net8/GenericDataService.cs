using Microsoft.EntityFrameworkCore;
using StockData.Net8.Services.Common;
using StockService.Net8.Models;
using StockService.Net8.Services;

namespace StockData.Net8;

public class GenericDataService<T>(StockDataDbContext dbContext, NonQueryDataService<T> nonQueryDataService) : IDataService<T> where T : DomainObject
{
    private readonly StockDataDbContext _dbContext = dbContext;
    private readonly NonQueryDataService<T> _nonQueryDataService = nonQueryDataService;

    public async Task<T> Create(T entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<T> Get(int id)
    {
       T? entity = await _dbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
       
        return entity;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IEnumerable<T> entities = await _dbContext.Set<T>().ToListAsync();
     
        return entities;
    }

    public async Task<T> Update(int id, T entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}
