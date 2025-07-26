using Microsoft.EntityFrameworkCore;

using Application.Framework;

namespace InMemoryDb.DataEntity;

public class InMemoryDbContext : BaseInMemoryDbContext
{
    public InMemoryDbContext() : base()
    { }

    public InMemoryDbContext(string dbName) : base(dbName)
    { }

    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
    { }

    public DbSet<HouseEntity> Houses => Set<HouseEntity>();
    public DbSet<BidEntity> Bids => Set<BidEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<SpeakerEntity> Speakers => Set<SpeakerEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey(x => x.ProductId).IsClustered(true);
            entity.HasIndex(x => x.Id).IsUnique(true);
            entity.Property(x => x.ProductId).ValueGeneratedOnAdd();
            entity.Property(x => x._tagList).HasColumnName("TagList");
            entity.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();
        });
        base.OnModelCreating(modelBuilder);
    }
}
