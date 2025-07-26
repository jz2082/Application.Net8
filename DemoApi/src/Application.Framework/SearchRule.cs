namespace Application.Framework;

public record SearchRule : BaseRecord
{
    public string Label { get; init; }
    public string Condition { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public string MatchMode { get; init; }
    public string SearchValue { get; init; }
}
