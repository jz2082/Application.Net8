namespace TestConsole.Net8;

public record AppSetting
{
    public string Environment { get; init; }
    public string DbConnection { get; init; }
    public string CommandTimeout { get; init; }
    public string AboutMessage { get; init; }
}
