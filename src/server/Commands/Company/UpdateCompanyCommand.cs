using Core;

namespace Commands;

public class UpdateCompanyCommand : Command
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public Guid OwnerId{ get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}

/*public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();
    }
}*/