using Core;

namespace Commands;

public class CreateOrbitCommand : Command<CreateOrbitResponse>
{
    public string Title { get; set; }
    public Guid CompanyId { get; set; }
    //public Guid MainConsultantId { get; set; }
    public int StartYear { get; set; }
    public Month StartMonth { get; set; }
    public int YearDuration { get; set; }
}
public class CreateOrbitResponse
{
    public Guid OrbitId { get; set; }
    public long OrbitKey { get; set; }
    //public Guid MainParticipantConsultantId { get; set; }
    public IEnumerable<YearValue> Years { get; set; }
}

public class YearValue
{
    public Guid YearId { get; set; }
    public long YearKey { get; set; }
    public int Value { get; set; }
}

/*public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();
    }
}*/