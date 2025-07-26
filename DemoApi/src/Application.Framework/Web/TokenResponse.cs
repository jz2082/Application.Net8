using Newtonsoft.Json;

namespace Application.Framework;

public record TokenResponse : BaseRecord
{
    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }

    public string ext_expires_in { get; set; }

    [JsonProperty("expires_on")]
    public string ExpiresOn { get; set; }

    public string not_before { get; set; }

    [JsonProperty("resource")]
    public string Resource { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("id_token")]
    public string IdToken { get; set; }
}
