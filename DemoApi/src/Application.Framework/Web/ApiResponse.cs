using Newtonsoft.Json;

namespace Application.Framework;

public record ApiResponse<T> : BaseRecord
{
    public string ExceptionMessage { get; set; }
    public bool HasException => !string.IsNullOrEmpty(ExceptionMessage);
    public AdditionalInfo RuleViolation => new ();
    public bool HasViolation => RuleViolation != null && RuleViolation.Value.Count > 0;
    [JsonProperty("data")]
    public T? Data { get; set; }
}