using Commands;
using Core;
using Domain;
using System;

namespace CommandHandlers;

public class CompanyCommandHandlers : CommandHandlerBase,
    ICommandHandler<CreateCompanyCommand, CreateCompanyResponse>,
    ICommandHandler<AddOwnerToCompanyCommand, AddPersonnelToCompanyResponse>,
    ICommandHandler<AddWhoToCompanyCommand, AddPersonnelToCompanyResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly ICompanyDomainService _companyDomainService;

    public CompanyCommandHandlers(
        IUserDomainService userDomainService, 
        ICompanyDomainService companyDomainService)
    {
        _userDomainService = userDomainService;
        _companyDomainService = companyDomainService;
    }

    public async Task<CreateCompanyResponse> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = _companyDomainService.CreateCompany(command.Name, PhoneNumber.Of(command.PhoneNumber), Email.Of(command.Email), command.Address);
        await Database.SaveChanges(cancellationToken);
        return new CreateCompanyResponse
        {
            CompanyId = company.Id,
        };
    }
    public async Task<AddPersonnelToCompanyResponse> Handle(AddOwnerToCompanyCommand command, CancellationToken cancellationToken)
    {
        var user = await ResolveUser(command.Email,FullName.Of(command.FirstName,command.LastName));
        var person = await _companyDomainService.AddOwner(command.CompanyId, user);
        await Database.SaveChanges(cancellationToken);
        return ToAddPersonnelToCompanyResponse(person);
    }
    public async Task<AddPersonnelToCompanyResponse> Handle(AddWhoToCompanyCommand command, CancellationToken cancellationToken)
    {
        var user = await ResolveUser(command.Email, FullName.Of(command.FirstName, command.LastName));
        var person = await _companyDomainService.AddWho(command.CompanyId, user);
        await Database.SaveChanges(cancellationToken);
        return ToAddPersonnelToCompanyResponse(person);
    }

    private async Task<List<User>> ResolveUsers(string[] emails)
    {
        var users = new List<User>();
        foreach (var email in emails)
        {
            var user = await _userDomainService.ResolveUserByEmail(Email.Of(email));
            users.Add(user);
        }
        return users;
    }

    private async Task<User> ResolveUser(string email, FullName fullName)
    {
        return await _userDomainService.ResolveUserByEmail(Email.Of(email),fullName);
    }

    
    private AddPersonnelToCompanyResponse ToAddPersonnelToCompanyResponse(Person person)
        => new AddPersonnelToCompanyResponse
        {
            Email = person.User.Email,
            Role = person.Role,
            PersonId = person.Id
        };
}