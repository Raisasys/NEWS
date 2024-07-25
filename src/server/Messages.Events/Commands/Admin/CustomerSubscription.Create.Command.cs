using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class CreateCustomerSubscriptionCommand : AdminCommandBase
{
    public Guid CustomerId{ get; set; }
    public Guid SubscriptionId { get; set; }
}

public class CreateCustomerSubscriptionCommandValidator : AbstractValidator<CreateCustomerSubscriptionCommand>
{
    public CreateCustomerSubscriptionCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.SubscriptionId)
            .NotNull()
            .NotEmpty();

    }
}