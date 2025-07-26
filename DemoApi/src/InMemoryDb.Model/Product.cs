using Newtonsoft.Json;

using Application.Framework;

namespace InMemoryDb.Model;

public record Product : BaseModel<Guid>
{
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public string ProductCode { get; init; }
    internal string _tagList;
    public string[]? TagList
    {
        get { return string.IsNullOrEmpty(_tagList) ? null : JsonConvert.DeserializeObject<string[]>(_tagList); }
        set { _tagList = JsonConvert.SerializeObject(value); }
    }
    public DateTime ReleaseDate { get; init; }
    public double Price { get; init; }
    public string Description { get; init; }
    public double StarRating { get; init; }
    public string ImageUrl { get; init; }
}
