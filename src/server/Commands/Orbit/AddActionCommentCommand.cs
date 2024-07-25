using Core;

namespace Commands;

public class AddActionCommentCommand : Command<AddActionCommentResponse>
{
    public Guid OrbitId { get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public Guid ActionId { get; set; }
    public string Comment { get; set; }
}

public class AddActionCommentResponse
{
    public Guid CommentId { get; set; }
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