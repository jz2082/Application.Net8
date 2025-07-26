using Application.Framework;

namespace InMemoryDb.Model;

public record UserAuthZ : BaseModel<int>
{
  public string UserId { get; set; } = string.Empty;
  public string Type { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;
}