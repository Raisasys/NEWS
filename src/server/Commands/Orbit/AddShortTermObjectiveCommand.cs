using Core;

namespace Commands;

public class AddShortTermObjectiveCommand : Command<AddShortTermObjectiveResponse>
{
    public Guid OrbitId{ get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public ShortTermObjectiveValue[] ShortTermObjectiveItems { get; set; }
}
public class ShortTermObjectiveValue
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid YearId{ get; set; }
    public Guid OwnerPersonId { get; set; }
}

public class AddShortTermObjectiveResponse
{
    public IEnumerable<ShortTermObjectiveValueResponse> ObjectiveItems { get; set; }
}

public class ShortTermObjectiveValueResponse
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public Guid YearId { get; set; }
    public Guid PersonId { get; set; }
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