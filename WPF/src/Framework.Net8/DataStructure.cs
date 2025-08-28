namespace Framework.Net8;

public struct AdditionalInfoType
{
    public const int NoExecutionTime = 0;
    public const int TotalSeconds = 1;
    public const int TotalMilliseconds = 2;
}

public struct AppLogger
{
    public const string Application = "Application";
    public const string Module = "Module";
    public const string Method = "Method";
    public const string Message = "Message";
    public const string RenderedType = "RenderedType";
    public const string UserName = "UserName";
}

public struct AppLogicGate
{
    public const string And = "and";
    public const string Or = "or";
}

public struct AppFilterType
{
    public const string String = "string";
    public const string Text = "text";
    public const string DropDown = "dropDown";
    public const string Double = "double";
    public const string Numeric = "numeric";
    public const string Integer = "integer";
    public const string DateTime = "dateTime";
    public const string Boolean = "boolean";
    public const string Guid = "Guid";
}

public struct AppFilterMode
{
    public const string StartsWith = "StartsWith";
    public const string Contains = "Contains";
    public const string NotContains = "NotContains";
    public const string EndsWith = "EndsWith";
    public const string ValueEquals = "Equals";
    public const string ValueNotEquals = "NotEquals";
    public const string ValueIn = "in";
    public const string LessThan = "lt";
    public const string LessThenEqual = "lte";
    public const string GreaterThan = "gt";
    public const string GreaterThanEqual = "gte";
    public const string DateIs = "is";
    public const string DateIsNot = "isNot";
    public const string DateBefore = "before";
    public const string DateAfter = "after";
}


