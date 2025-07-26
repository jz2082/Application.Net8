namespace Application.Framework;

public record SortCondition : BaseRecord
{
    public string Name { get; init; }
    public int Order { get; init; }
}