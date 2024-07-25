using Core;

namespace Commands;

public class AddOwnerToCompanyCommand : Command<AddPersonnelToCompanyResponse>
{
    public Guid CompanyId{ get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
public class AddWhoToCompanyCommand : Command<AddPersonnelToCompanyResponse>
{
    public Guid CompanyId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


public class AddPersonnelToCompanyResponse
{
    public string Email { get; set; }
    public PersonRole Role { get; set; }
    public Guid PersonId { get; set; }
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