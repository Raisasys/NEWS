using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class ActiveTrialCustomerSubscriptionCommand : AdminCommandBase
{
    public Guid CustomerId{ get; set; }
}

public class ActiveTrialCustomerSubscriptionCommandValidator : AbstractValidator<ActiveTrialCustomerSubscriptionCommand>
{
    public ActiveTrialCustomerSubscriptionCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();
        
    }
}