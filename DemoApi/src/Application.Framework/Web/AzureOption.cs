namespace Application.Framework;

public record AzureOption : OidcOption
{
    public string TenantId { get; set; }
    public string Scopes { get; set; }
}
