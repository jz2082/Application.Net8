using Microsoft.EntityFrameworkCore;
using StockService.Net8.Models;

namespace StockAppData.Net8;

public class StockDataInMemoryDbContext : BaseInMemoryDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public StockDataInMemoryDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);

        base.OnModelCreating(modelBuilder);
    }
}
