using Core;

namespace Commands;

public class CreateConsultantCommand : Command<CreateConsultantResponse>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
public class CreateConsultantResponse
{
    public Guid ConsultantId { get; set; }
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