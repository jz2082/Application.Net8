using Microsoft.EntityFrameworkCore;
using StockService.Net8.Models;

namespace StockData.Net8;

public class StockDataDbContext : BaseInMemoryDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public StockDataDbContext(DbContextOptions options) : base(options) { }

    public StockDataDbContext(string dbName) : base()
    {
        _dbName = dbName;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);

        base.OnModelCreating(modelBuilder);
    }
}
