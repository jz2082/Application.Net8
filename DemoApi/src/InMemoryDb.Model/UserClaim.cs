using System.Text.Json.Serialization;

using Application.Framework;

namespace InMemoryDb.Model;

public record UserClaim : BaseRecord
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("value")]
    public object Value { get; set; } = string.Empty;
}