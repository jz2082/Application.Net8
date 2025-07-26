using Microsoft.EntityFrameworkCore;

namespace Application.Framework;

public class InMemoryDbHealthCheckContext : BaseInMemoryDbContext
{
    public InMemoryDbHealthCheckContext() : base()
    { }

    public InMemoryDbHealthCheckContext(string dbName) : base()
    {
        _dbName = dbName;
    }

    public InMemoryDbHealthCheckContext(DbContextOptions<InMemoryDbHealthCheckContext> options) : base(options)
    { }
}
