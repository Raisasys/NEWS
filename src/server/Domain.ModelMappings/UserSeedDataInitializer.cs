using Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.ModelMappings;

public class A00_UserSeedDataInitializer : SeedDataInitializer
{
    public override async Task Initialize(IUowDatabase database)
    {
        try
        {
            if(await database.Set<User>().AnyAsync()) return;

            await Add(database, Email.Of("admin@orbit.info"), new FullName("admin", "admin"));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task<User> Add(IUowDatabase database, Email email, FullName fullName)
    {
        var user = new User(email,fullName);
        user.CreatedBy = "admin@orbit.info";
        database.Add(user);

        await database.SaveChanges();
        return user;
    }

}