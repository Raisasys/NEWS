using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class CreateSubscriptionCommand : AdminCommandBase
{
    public string Name { get; set; }
    public int NumberOfDays { get; set; }
    public int InitPrice { get; set; }
    public int FinalPrice { get; set; }
    public int? GstPrice { get; set; } // 10%
    public int? UsageCeilingNumber { get; set; }
    public string Description { get; set; }
}

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.NumberOfDays)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.InitPrice)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.FinalPrice)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);


    }
}