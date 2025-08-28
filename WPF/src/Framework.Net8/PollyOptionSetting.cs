namespace Application.Framework;

public record PollyOptionSetting 
{
    public int RetryCount { get; set; }
    public int RetryDelayMilliSeconds { get; set; }
    public int NumberOfBreakEvent { get; init; }
    public int DurationOfBreak { get; init; }
}