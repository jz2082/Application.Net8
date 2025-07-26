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

    public override int SaveChanges()
    {
        foreach (var history in ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity<Guid> && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as BaseEntity<Guid>))
        {
            if (history != null)
            {
                if (history.Id == Guid.Empty) history.Id = Guid.NewGuid();
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue) history.DateCreated = DateTime.Now;
            }
        }
        return base.SaveChanges(true);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var history in ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity<Guid> && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as BaseEntity<Guid>))
        {
            if (history != null)
            {
                if (history.Id == Guid.Empty) history.Id = Guid.NewGuid();
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue) history.DateCreated = DateTime.Now;
            }
        }
        return await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
    }
}