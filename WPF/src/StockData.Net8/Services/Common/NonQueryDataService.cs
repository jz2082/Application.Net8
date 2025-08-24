using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StockService.Net8.Models;

namespace StockData.Net8.Services.Common;

public class NonQueryDataService<T>(StockDataDbContext context) where T : DomainObject
{
    private readonly StockDataDbContext _context = context;

    public async Task<T> Create(T entity)
    {
        EntityEntry<T> createdResult = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return createdResult.Entity;
    }

    public async Task<T> Update(int id, T entity)
    {
        entity.Id = id;
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        T? entity = await _context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        return true;
    }
}