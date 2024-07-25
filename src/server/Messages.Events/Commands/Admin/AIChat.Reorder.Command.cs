using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class ReorderAIChatCommand : AdminCommandBase
{
    public Guid CurrentId { get; set; }
    public Guid BeforeId { get; set; }
}

public class ReorderAIChatCommandValidator : AbstractValidator<ReorderAIChatCommand>
{
    public ReorderAIChatCommandValidator()
    {
        RuleFor(x => x.CurrentId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.BeforeId)
            .NotNull()
            .NotEmpty();


    }
}