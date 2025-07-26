namespace Application.Framework;

public record PaginatedData<T> where T : BaseRecord
{
    public int Index { get; set; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public bool HasFirstLastPage { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }
    public int PageCount { get; init; }
    public long Count { get; init; }
    public IEnumerable<T>? Data { get; init; }

    public PaginatedData()
    {
        Index = 1;
        PageIndex = 1;
        PageSize = 1;
        Count = 0;
        Data = null;
    }

    public PaginatedData(int index, int pageSize, long count, IEnumerable<T>? data = null, bool hasNextPage = false)
    {
        Index = index;
        PageIndex = (index + pageSize) / pageSize;
        PageSize = pageSize;
        Count = count;
        Data = data;
        // calculate other properties
        if (Count > 0)
        {
            PageCount = Convert.ToInt32(Count / PageSize);
            if (Count % pageSize > 0) PageCount++;
            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex < PageCount;
            PageIndex = PageIndex > PageCount ? PageCount : PageIndex;
            HasFirstLastPage = PageCount > 1;
        }
        else
        {
            HasPreviousPage = PageIndex > 1;
            HasNextPage = hasNextPage;
            HasFirstLastPage = false;
        }
    }
}