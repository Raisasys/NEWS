using Commands;
using Core;
using Domain;

namespace CommandHandlers;

public class ConsultantCommandHandlers : CommandHandlerBase,
    ICommandHandler<CreateConsultantCommand, CreateConsultantResponse>
{
    private readonly IUserDomainService _userDomainService;

    public ConsultantCommandHandlers(IUserDomainService userDomainService)
    {
        _userDomainService = userDomainService;
    }

    public async Task<CreateConsultantResponse> Handle(CreateConsultantCommand command, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.ResolveUserByEmail(Email.Of(command.Email),FullName.Of(command.FirstName,command.LastName));
        var consultant = new Consultant(new UserValue(user.Id,user.FullName));
        Database.Add(consultant);
        await Database.SaveChanges(cancellationToken);
        return new CreateConsultantResponse
        {
            ConsultantId = consultant.Id
        };
    }
}