using Application.Framework;
using System.ComponentModel.DataAnnotations;

namespace InMemoryDb.Model;

public record House : BaseModel<int>
{
    [property: Required]
    public string? Address { get; init; }
    [property: Required]
    public string? Country { get; init; }
    public string? Description { get; init; }
    public int Price { get; init; }
    public string? Photo { get; init; }
}
