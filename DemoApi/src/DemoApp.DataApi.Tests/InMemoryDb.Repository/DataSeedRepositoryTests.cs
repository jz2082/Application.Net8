using System.Threading.Tasks;
using Xunit;

using InMemoryDb.DataEntity;

namespace Demo.InMemoryDb.Repository;

public class DataSeedRepositoryTests
{
    [Fact]
    public async Task DataSeedAsync_ShouldSeedAllEntities()
    {
        // Arrange
        string dbName = Guid.NewGuid().ToString();

        // Act
        await DataSeedRepository.DataSeedAsync(dbName);

        // Assert
        using var dbContext = new InMemoryDbContext(dbName);
        Assert.Equal(5, await dbContext.Houses.CountAsync());
        Assert.Equal(12, await dbContext.Bids.CountAsync());
        Assert.Equal(2, await dbContext.Users.CountAsync());
        Assert.Equal(9, await dbContext.Speakers.CountAsync());
        Assert.Equal(5, await dbContext.Products.CountAsync());
    }

    [Fact]
    public async Task DataSeedAsync_ShouldThrowException_WhenDbNameIsNull()
    {
        // Arrange & Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => DataSeedRepository.DataSeedAsync(null));
    }
}