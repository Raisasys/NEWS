using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class UpdateSubscriptionCommand : AdminCommandBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfDays { get; set; }
    public int InitPrice { get; set; }
    public int FinalPrice { get; set; }
    public int? GstPrice { get; set; } // 10%
    public int? UsageCeilingNumber { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; }
}

public class UpdateSubscriptionCommandValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

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