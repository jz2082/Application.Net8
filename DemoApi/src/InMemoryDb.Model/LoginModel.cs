using Application.Framework;

namespace InMemoryDb.Model;

public record LoginModel : BaseModel<int>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberLogin { get; set; }
}
