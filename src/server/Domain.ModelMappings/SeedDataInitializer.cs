using Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.ModelMappings;

public class A00_NewsSeedDataInitializer : SeedDataInitializer
{
    public override async Task Initialize(IUowDatabase database)
    {
        if (await database.Set<News>().AnyAsync()) return;

        
    }

}