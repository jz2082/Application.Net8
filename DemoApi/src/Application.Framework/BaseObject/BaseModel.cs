namespace Application.Framework;

public abstract record BaseModel<S> : BaseRecord where S : struct
{
    public S Id { get; init; }
    public string RelationId { get; set; }
    public byte[] RowVersion { get; set; }
}