using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class UpdateAIChatCommand : AdminCommandBase
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string AIModelName { get; set; }
    public string AIObjectType { get; set; }
    public int MaxTokens { get; set; }
    public string ApiKey { get; set; }
    public bool Enabled { get; set; }
    public bool IsPremium { get; set; }
}

public class UpdateAIChatCommandValidator : AbstractValidator<UpdateAIChatCommand>
{
    public UpdateAIChatCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.AIModelName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.AIObjectType)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.MaxTokens)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.ApiKey)
            .NotNull()
            .NotEmpty();

    }
}