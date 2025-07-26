using Application.Framework;
using System.ComponentModel.DataAnnotations;

namespace InMemoryDb.Model;

public record Bid : BaseModel<int>
{
    public int HouseId { get; set; }

    [Required]
    public string Bidder { get; set; } = string.Empty;
    public int Amount { get; set; }
}