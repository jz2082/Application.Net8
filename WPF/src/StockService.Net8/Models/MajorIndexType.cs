namespace StockService.Net8.Models;

public enum MajorIndexType
{
    DowJones,
    Nasdaq,
    SP500
}

public class MajorIndex
{
    public string IndexName { get; set; }
    public double Price { get; set; }
    public double Changes { get; set; }
    public MajorIndexType Type { get; set; }
}
