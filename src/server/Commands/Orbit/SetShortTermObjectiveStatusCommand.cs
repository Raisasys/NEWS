using Core;

namespace Commands;

public class SetShortTermObjectiveStatusCommand : Command<SetShortTermObjectiveStatusResponse>
{
    public Guid OrbitId { get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public Month Month { get; set; }
    public Direction Direction { get; set; }
    public RAG RAG { get; set; }
}

public class SetShortTermObjectiveStatusResponse
{
    public Guid StatusId { get; set; }
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