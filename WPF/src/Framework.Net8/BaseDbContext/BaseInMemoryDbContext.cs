using Microsoft.EntityFrameworkCore;

namespace Application.Framework;

public class BaseInMemoryDbContext : DbContext
{
    protected string _dbName = string.Empty;

    public BaseInMemoryDbContext() : base()
    { }

    public BaseInMemoryDbContext(string dbName) : base()
    {
        _dbName = dbName;
    }

    public BaseInMemoryDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrEmpty(_dbName))
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(
                    databaseName: _dbName ?? "DemoDb", 
                    options => 
                        options.EnableNullChecks(true))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        base.OnConfiguring(optionsBuilder);
    }
}