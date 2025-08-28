namespace Application.Framework;

public record AppSetting
{
    public string Environment { get; init; }
    public string FinancialModelingApiKey { get; set; }
    public string DbConnection { get; init; }
    public string CommandTimeout { get; init; }
    public string AboutMessage { get; init; }
}
