using Core;
using System;
using Microsoft.EntityFrameworkCore;

namespace Domain.ModelMappings;

public class C00_CompanySeedDevDataInitializer : SeedDevDataInitializer
{
    public override async Task Initialize(IUowDatabase database)
    {
        try
        {
            await Add(database,
                "adam@companytest1.com", "Adam", "Prince", "+4454854548",
                "info@companytest1.com", "Company Test 1", Address.Of("UK", "London", "details", "54643545245"),
                "004421548795",
                "elliot@companytest1.com", "Elliot", "Mahout", "+4454854456"
            );

            await Add(database,
                "ali@companytest2.com", "Ali", "Shiri", "+985422005545",
                "info@companytest2.com", "Company Test 2", Address.Of("IR", "Tehran", "details", "98544545518"),
                "00982125415458",
                "mohsen@companytest2.com", "Mohsen", "Saeedi", "+98587054456"
            );
            
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task Add(IUowDatabase database, 
        string ownerEmail, string ownerFirstName, string ownerLastName, string ownerPhone,
        string companyEmail, string companyName, Address companyAddress, string companyPhone,
        string personEmail, string personFirstName, string personLastName, string personPhone)
    {
        var owner1 = await database.Find<User>(ownerEmail);
        if (owner1 == null)
        {
            owner1 = new User(Email.Of(ownerEmail), FullName.Of(ownerFirstName, ownerLastName), PhoneNumber.Of(ownerPhone), null);
            owner1.CreatedBy = "admin@orbit.info";
            database.Add(owner1);
        }

        var who1 = await database.Find<User>(personEmail);
        if (who1 == null)
        {
            who1 = new User(Email.Of(personEmail), FullName.Of(personFirstName, personLastName), PhoneNumber.Of(personPhone), null);
            who1.CreatedBy = "admin@orbit.info";
            database.Add(who1);
        }

        var company = await database.Set<Company>().SingleOrDefaultAsync(i => i.Email == companyEmail);
        if (company == null)
        {
            company = new Company(
                companyName,
                PhoneNumber.Of(companyPhone),
                Email.Of(companyEmail),
                companyAddress);
            company.CreatedBy = "admin@orbit.info";
            database.Add(company);

            var personOwner = new Person(new UserValue(owner1.Id, owner1.FullName), PersonRole.Both);
            personOwner.CreatedBy = "admin@orbit.info";
            company.Personnel.Add(personOwner);

            var personWho1 = new Person(new UserValue(who1.Id, who1.FullName), PersonRole.Who);
            personWho1.CreatedBy = "admin@orbit.info";
            company.Personnel.Add(personWho1);
        }

        await database.SaveChanges();
    }
}