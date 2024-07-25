using Core;

namespace Commands;

public class CreateCompanyCommand : Command<CreateCompanyResponse>
{
    public string Name { get; set; }
    //public string OwnerEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}
public class CreateCompanyResponse
{
    public Guid CompanyId{ get; set; }
    //public Guid OwnerId { get; set; }
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