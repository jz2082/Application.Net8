namespace Application.Framework;

public record HealthCheckOptionSetting : BaseRecord
{
    public long MemoryThreshold { get; set; }
    public string HttpCheckBaseAddress { get; set; }
    public string HttpCheckEndPoint { get; set; }
    public string SqlDbConnection { get; set; }
    public string InMemoryDbConnection { get; set; }
}