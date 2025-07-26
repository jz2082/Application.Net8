using Application.Framework;

namespace InMemoryDb.Model;

public record UserModel : BaseModel<int>
{
    public string? Name { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? FavoriteColor { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty;
    public string? GoogleId { get; set; } = string.Empty;
}
