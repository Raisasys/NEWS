/*namespace Domain;

public record MainConsultant(Guid ConsultantId)
{
    public MainConsultant() : this(Guid.Empty) { }

    public Guid ConsultantId { get; set; } = ConsultantId;

    public static MainConsultant Of(Guid? value)
    {
        return value.HasValue ? new MainConsultant(value.Value) : null;
    }
}*/