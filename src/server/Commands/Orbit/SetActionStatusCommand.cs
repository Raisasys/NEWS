using Core;

namespace Commands;

public class SetActionStatusCommand : Command<SetActionStatusResponse>
{
    public Guid OrbitId { get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public Guid ActionId { get; set; }
    public Direction Direction { get; set; }
    public RAG RAG { get; set; }
}

public class SetActionStatusResponse
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