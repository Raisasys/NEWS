using Core;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Domain.ModelMappings;

public class D00_ConsultantSeedDevDataInitializer : SeedDevDataInitializer
{
    public override async Task Initialize(IUowDatabase database)
    {
        try
        {
            if(await database.Set<Orbit>().AnyAsync()) return;

            var companyItems = await database.Set<Company>().ToListAsync();
            var consultantItems = await database.Set<Consultant>().ToListAsync();

            var index = 0;
            foreach (var companyItem in companyItems)
            {
                var consultantItem = consultantItems[index];
                var title = $"Orbit {index + 1}";
                var orbit = await AddOrbit(database, title, companyItem, consultantItem);
                await AddLongTermObjective(database, orbit);
                await AddShortTermObjective(database, orbit, companyItem);
                await AddActions(database, orbit, companyItem);
                index++;
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task<Orbit> AddOrbit(IUowDatabase database, string title, Company company,Consultant consultant)
    {
        var orbit = await database.Set<Orbit>().FirstOrDefaultAsync(i=>i.Title == title);
        if (orbit == null)
        {
            orbit = new Orbit(title,new OwnerCompany(company.Id, company.Name), 2024, Month.January, 3);
            orbit.CreatedBy = "admin@orbit.info";
            database.Add(orbit);
        }

        var participantConsultant = new ParticipantConsultant(consultant.Id, ConsultantPermission.Owner);
        participantConsultant.CreatedBy = "admin@orbit.info";
        orbit.Consultants.Add(participantConsultant);
        await database.SaveChanges();
        return orbit;
    }

    private async Task AddLongTermObjective(IUowDatabase database, Orbit orbit)
    {
        for (int i = 1; i < 13; i++)
        {
            var objective = new LongTermObjective(Guid.NewGuid(), $"Long Term {i}", i);
            objective.CreatedBy = "admin@orbit.info";
            orbit.LongTermObjectives.AddEntity(objective);
        }

        //database.Update(orbit);
        await database.SaveChanges();
    }

    private async Task AddShortTermObjective(IUowDatabase database, Orbit orbit, Company company)
    {
        var owners = company.Personnel.Where(i => i.Role == PersonRole.Owner || i.Role == PersonRole.Both).ToList();
        
        foreach (var longTermObjective in orbit.LongTermObjectives)
        {
            var ownerIndex = 0;
            foreach (var year in orbit.Years)
            {
                var selectOwner = owners[ownerIndex];
                if (ownerIndex + 1 < owners.Count) ownerIndex++;

                var shortObjective = new ShortTermObjective(Guid.NewGuid(), $"Short Term {year.Value}", year, new ObjectiveOwner(selectOwner.Id));
                shortObjective.CreatedBy = "admin@orbit.info";
                longTermObjective.ShortTermObjectives.AddEntity(shortObjective);
            }
        }
        //database.Update(orbit);
        await database.SaveChanges();
    }

    private async Task AddActions(IUowDatabase database, Orbit orbit, Company company)
    {
        var whos = company.Personnel.Where(i => i.Role == PersonRole.Who || i.Role == PersonRole.Both).ToList();

        foreach (var longTermObjective in orbit.LongTermObjectives)
        {
            foreach (var shortTermObjective in longTermObjective.ShortTermObjectives)
            {
                var whoIndex = 0;
                for (int i = 1; i < 4; i++)
                {
                    var selectWho= whos[whoIndex];
                    if (whoIndex + 1 < whos.Count) whoIndex++;

                    var action = new ObjectiveAction(Guid.NewGuid(), $"Action {i}", i, DateTime.Now, selectWho.Id, 2, 3, shortTermObjective.Year.Value, Month.February, 2, shortTermObjective.Year.Value, Month.May, 3);
                    action.CreatedBy = "admin@orbit.info";
                    shortTermObjective.Actions.AddEntity(action);
                }
            }
        }
        //database.Update(orbit);
        await database.SaveChanges();
    }
}