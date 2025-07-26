namespace Application.Framework;

public interface IViolationInfo
{
    string ExceptionMessage { get; set; }
    bool HasException => !string.IsNullOrEmpty(ExceptionMessage);
    AdditionalInfo RuleViolation { get; }
    bool HasViolation => RuleViolation != null && RuleViolation.Value.Count > 0;
}