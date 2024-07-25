using Core;

namespace Domain;

/*public record CompanyOwner(Guid PersonId)
{
    public CompanyOwner() : this(Guid.Empty) { }

    public Guid PersonId { get; set; } = PersonId;

    public static CompanyOwner Of(string value)
    {
        if (value.IsEmpty()) return null;
        return Guid.TryParse(value, out var id) ? new CompanyOwner(id) : null;
    }
    public static CompanyOwner Of(Guid? value)
    {
        return value.HasValue ? new CompanyOwner(value.Value) : null;
    }
}*/