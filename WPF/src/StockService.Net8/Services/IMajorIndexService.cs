using StockService.Net8.Models;

namespace StockService.Net8.Services;

public interface IMajorIndexService
{
    Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
}
