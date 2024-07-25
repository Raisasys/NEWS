using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;

public class ConfirmAHPRANumberAdminCommand : AdminCommandBase
{
    public Guid CustomerId { get; set; }
    public string AHPRARegistrationNumber { get; set; }
}


public class ConfirmAHPRANumberAdminCommandValidator : AbstractValidator<ConfirmAHPRANumberAdminCommand>
{
    public ConfirmAHPRANumberAdminCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();
    }
}