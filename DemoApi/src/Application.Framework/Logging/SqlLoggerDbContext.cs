using Microsoft.EntityFrameworkCore;

namespace Application.Framework.Logging;

public class SqlLoggerDbContext : BaseSqlDbContext
{
    public SqlLoggerDbContext() : base()
    { }

    public SqlLoggerDbContext(string connectionString) : base(connectionString)
    { }

    public SqlLoggerDbContext(DbContextOptions<SqlLoggerDbContext> options) : base(options)
    { }

    public DbSet<LoggerInfoEntity> LoggerInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // LoggerInfo
        modelBuilder.Entity<LoggerInfoEntity>(entity =>
        {
            entity.ToTable("LoggerInfo");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Application).IsUnique(false);
            entity.HasIndex(x => x.Module).IsUnique(false);
            entity.HasIndex(x => x.Method).IsUnique(false);
            entity.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();
        });
    }
}
