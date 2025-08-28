namespace Application.Framework.Logging;

public interface ILoggerInfoInMemoryRepository
{
    Task<IEnumerable<LoggerInfoEntity>?> GetAllAsync();
    Task<LoggerInfoEntity?> GetAsync(Guid id);
}
