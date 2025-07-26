namespace Application.Framework;

public record TokenInfo : BaseRecord
{
    public string UserId { get; set; }
    public string IdToken { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpiresOn { get; set; }
    public bool Enabled { get; set; }
}
