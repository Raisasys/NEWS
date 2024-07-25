using Core;

namespace Domain;

public interface ICompanyDomainService
{
    Company CreateCompany(string name, PhoneNumber phoneNumber, Email email, Address address);
    Task<IEnumerable<Person>> AddWhos(Guid companyId, params User[] users);
    Task<Person> AddWho(Guid companyId, User user);
    Task<IEnumerable<Person>> AddOwners(Guid companyId, params User[] users);
    Task<Person> AddOwner(Guid companyId, User users);
}

internal class CompanyDomainService : DomainService, ICompanyDomainService
{
    public Company CreateCompany(string name, PhoneNumber phoneNumber, Email email, Address address)
    {
        var company = new Company(name, phoneNumber, email, address);
        Database.Add(company);
        return company;
    }

    public async Task<IEnumerable<Person>> AddWhos(Guid companyId, params User[] users)
    {
        var persons = new List<Person>();
        var company = await Database.Find<Company>(companyId);
        foreach (var user in users)
        {
            var who = company.AddWho(user);
            persons.Add(who);
        }
        return persons;
    }

    public async Task<Person> AddWho(Guid companyId, User user)
    {
        var company = await Database.Find<Company>(companyId);
        var who = company.AddWho(user);
        return who;
    }

    public async Task<IEnumerable<Person>> AddOwners(Guid companyId, params User[] users)
    {
        var persons = new List<Person>();
        var company = await Database.Find<Company>(companyId);
        foreach (var user in users)
        {
            var owner = company.AddOwner(user);
            persons.Add(owner);
        }
        return persons;
    }

    public async Task<Person> AddOwner(Guid companyId, User user)
    {
        var company = await Database.Find<Company>(companyId);
        var owner = company.AddOwner(user);
        return owner;
    }
}

