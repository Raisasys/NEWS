namespace Domain;

public record OwnerCompany(Guid CompanyId,string CompanyName)
{
    public OwnerCompany() : this(Guid.Empty, string.Empty) { }

    public Guid Id { get; set; } = CompanyId;

    public string Name { get; set; } = CompanyName;
}