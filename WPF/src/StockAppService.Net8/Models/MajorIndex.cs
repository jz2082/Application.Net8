namespace StockService.Net8.Models;

public class MajorIndex
{
    public string Symbol { get; set; }
    public double Price { get; set; }
    public double Change { get; set; }
    public double Volume { get; set; }
    public MajorIndexType Type { get; set; }
}
