namespace Application.Framework;

public record AppSetting : BaseRecord
{
    public string Environment { get; init; }
    public string DbConnection { get; init; }
    public string CommandTimeout { get; init; }
    public string DatabaseName { get; init; }
    public string DownStreamApiUrl { get; init; }
    public string CertificateFileName { get; init; }
    public string CertificatePassword { get; init; }
    public string AboutMessage { get; init; }
}