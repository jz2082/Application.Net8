namespace Application.Framework;

public record OidcOption : BaseRecord
{
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Instance { get; set; }
    public string Domain { get; set; }
    public string Resource { get; set; }
    public string CallbackPath { get; set; }
}
