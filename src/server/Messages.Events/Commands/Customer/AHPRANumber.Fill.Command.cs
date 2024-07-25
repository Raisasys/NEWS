using Core;
using Domain;
using FluentValidation;

namespace Commands.Customer;

public class FillAHPRANumberCommand : CustomerCommandBase
{
    public string AHPRARegistrationNumber { get; set; }
}


public class FillAHPRANumberCommandValidator : AbstractValidator<FillAHPRANumberCommand>
{
    public FillAHPRANumberCommandValidator()
    {
        RuleFor(x => x.AHPRARegistrationNumber)
            .NotNull()
            .NotEmpty();
    }
}