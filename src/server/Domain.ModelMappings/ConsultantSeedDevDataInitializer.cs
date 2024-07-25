using Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domain.ModelMappings;

public class B00_ConsultantSeedDevDataInitializer : SeedDevDataInitializer
{
    public override async Task Initialize(IUowDatabase database)
    {
        try
        {
            await Add(database, "atir@consultant.com", "Atir", "Niutir", "+44545876520");
            await Add(database, "wajihe@consultant.com", "Wajihe", "Firoos", "+4454857485");
            await Add(database, "chad@consultant.com", "Chad", "Dehan", "+445528048");

            await database.SaveChanges();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task Add(IDatabase database, 
        string email, string firstName, string lastName, string phone)
    {
        var user = await database.Find<User>(email);
        if (user == null)
        {
            user = new User(Email.Of(email), FullName.Of(firstName, lastName), PhoneNumber.Of(phone), null);
            user.CreatedBy = "admin@orbit.info";
            database.Add(user);
        }
        
        var consultant = await database.Set<Consultant>().SingleOrDefaultAsync(i => i.User.Email == email);
        if (consultant == null)
        {
            consultant = new Consultant(new UserValue(email,user.FullName));
            consultant.CreatedBy = "admin@orbit.info";
            database.Add(consultant);
        }
    }
}