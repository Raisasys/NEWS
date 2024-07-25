namespace Domain;

public record ActionWho(Guid PersonId)
{
    public ActionWho() : this(Guid.Empty) { }

    public Guid PersonId { get; set; } = PersonId;

    public static ActionWho Of(Guid? value)
    {
        return value.HasValue ? new ActionWho(value.Value) : null;
    }
}