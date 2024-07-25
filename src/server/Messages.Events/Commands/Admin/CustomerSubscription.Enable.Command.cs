using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class EnableCustomerSubscriptionCommand : AdminCommandBase
{
    public Guid CustomerId{ get; set; }
    public Guid CustomerSubscriptionId { get; set; }
}

public class EnableCustomerSubscriptionCommandValidator : AbstractValidator<EnableCustomerSubscriptionCommand>
{
    public EnableCustomerSubscriptionCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CustomerSubscriptionId)
            .NotNull()
            .NotEmpty();

    }
}