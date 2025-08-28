using Microsoft.EntityFrameworkCore;

namespace Framework.Net8.Logging;

public class InMemoryLoggerDbContext : BaseInMemoryDbContext
{
    public InMemoryLoggerDbContext() : base()
    { }

    public InMemoryLoggerDbContext(string dbName) : base()
    {
        _dbName = dbName;
    }

    public InMemoryLoggerDbContext(DbContextOptions<InMemoryLoggerDbContext> options) : base(options)
    { }

    public DbSet<LoggerInfoEntity> LoggerInfos => Set<LoggerInfoEntity>();
}
