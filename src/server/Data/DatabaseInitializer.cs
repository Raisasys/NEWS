using Core;

namespace Data;

public class DatabaseInitializer : Initializer
{
    protected override async Task Do()
    {
        var database = Context.GetDatabase();
        var seedDataInitializerTypes = TypeProvider.SeedDataInitializerTypes.OrderBy(i => i.Name);
        foreach (var seedDataInitializerType in seedDataInitializerTypes)
        {
            var seedDataInitializerInstance = Activator.CreateInstance(seedDataInitializerType);
            var initializeMethod = seedDataInitializerType.GetMethod(nameof(SeedDataInitializer.Initialize));
            Task result = (Task)initializeMethod.Invoke(seedDataInitializerInstance, new[] { database });
            await result;
        }


        if (Config.IsDevelopment)
        {
            var seedDevDataInitializerTypes = TypeProvider.SeedDevDataInitializerTypes.OrderBy(i => i.Name);
            foreach (var seedDevDataInitializerType in seedDevDataInitializerTypes)
            {
                var seedDataInitializerInstance = Activator.CreateInstance(seedDevDataInitializerType);
                var initializeMethod = seedDevDataInitializerType.GetMethod(nameof(SeedDevDataInitializer.Initialize));
                Task result = (Task)initializeMethod.Invoke(seedDataInitializerInstance, new[] { database });
                await result;
            }
        }


        database.Dispose();
    }
}
