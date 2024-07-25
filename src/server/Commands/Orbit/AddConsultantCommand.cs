using Core;

namespace Commands;

public class AddConsultantCommand : Command<AddConsultantResponse>
{
    public Guid OrbitId{ get; set; }
    public Guid ConsultantId { get; set; }
    public ConsultantPermission Permission { get; set; }
}

public class AddConsultantResponse
{
    public Guid ParticipantConsultantId { get; set; }
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