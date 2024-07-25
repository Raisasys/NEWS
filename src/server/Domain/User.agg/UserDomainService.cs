using Core;

namespace Domain;

public interface IUserDomainService
{
    Task<User> ResolveUserByEmail(Email email);
    Task<User> ResolveUserByEmail(Email email,FullName fullName);
}

internal class UserDomainService : DomainService, IUserDomainService
{
    public async Task<User> ResolveUserByEmail(Email email)
    {
        var user = await Database.Find<User>(email.Value);
        if (user == null)
        {
            user = new User(email);
            Database.Add(user);
        }
        return user;
    }
    public async Task<User> ResolveUserByEmail(Email email, FullName fullName)
    {
        var user = await Database.Find<User>(email.Value);
        if (user == null)
        {
            user = new User(email,fullName);
            Database.Add(user);
        }
        return user;
    }
}