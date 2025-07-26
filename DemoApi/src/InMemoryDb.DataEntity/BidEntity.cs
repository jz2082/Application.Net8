using Application.Framework;

namespace InMemoryDb.DataEntity;

public record BidEntity : BaseEntity<int>
{
    public int HouseId { get; set; }
    public HouseEntity? House { get; set; }
    public string Bidder { get; set; } = string.Empty;
    public int Amount { get; set; }
}