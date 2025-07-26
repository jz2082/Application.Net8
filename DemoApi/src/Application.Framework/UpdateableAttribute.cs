namespace Application.Framework;

[AttributeUsage(AttributeTargets.Property)]
public class UpdateableAttribute : Attribute
{
    public UpdateableAttribute()
    { }
}
