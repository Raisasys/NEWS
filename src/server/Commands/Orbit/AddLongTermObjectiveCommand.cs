using Core;

namespace Commands;

public class AddLongTermObjectiveCommand : Command<AddLongTermObjectiveResponse>
{
    public Guid OrbitId{ get; set; }
    public LongTermObjectiveValue[] ObjectiveItems { get; set; }
}

public class LongTermObjectiveValue
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Order{ get; set; }
}

public class AddLongTermObjectiveResponse
{
    public IEnumerable<LongTermObjectiveValueResponse> ObjectiveItems { get; set; }
}

public class LongTermObjectiveValueResponse
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
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