using Microsoft.EntityFrameworkCore;

namespace Application.Framework;

public class BaseSqlDbContext : DbContext
{
    private readonly string _connectionString = string.Empty;

    public BaseSqlDbContext() : base()
    { }

    public BaseSqlDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public BaseSqlDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrEmpty(_connectionString))
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(
                    _connectionString, 
                    sqlOptions => 
                        sqlOptions.EnableRetryOnFailure( 
                            maxRetryCount: 3, 
                            maxRetryDelay: TimeSpan.FromSeconds(30), 
                            errorNumbersToAdd: null))
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