using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class DisableCustomerSubscriptionCommand : AdminCommandBase
{
    public Guid CustomerId{ get; set; }
    public Guid CustomerSubscriptionId { get; set; }
}

public class DisableCustomerSubscriptionCommandValidator : AbstractValidator<DisableCustomerSubscriptionCommand>
{
    public DisableCustomerSubscriptionCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CustomerSubscriptionId)
            .NotNull()
            .NotEmpty();

    }
}