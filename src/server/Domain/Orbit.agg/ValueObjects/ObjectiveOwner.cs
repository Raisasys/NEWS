namespace Domain;

public record ObjectiveOwner(Guid PersonId)
{
    public ObjectiveOwner() : this(Guid.Empty) { }

    public Guid PersonId { get; set; } = PersonId;

    public static ObjectiveOwner Of(Guid? value)
    {
        return value.HasValue ? new ObjectiveOwner(value.Value) : null;
    }
}