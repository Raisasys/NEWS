using System.Reflection;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase<TEntity, TMappings>(this IServiceCollection services, Assembly migrationsAssembly) 
        where TEntity : Entity
        where TMappings : IEntityMapperBase
    {
        TypeProvider.SetEntitiesAssembly<TEntity>();
        TypeProvider.SetModelMappingsAssembly<TMappings>();
        services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Config.GetConnectionString(),
                    optionsBuilder =>
                    {
                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                        optionsBuilder.CommandTimeout(minutes);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.MigrationsAssembly(migrationsAssembly.FullName);
                    });
                options.AddInterceptors(new FixManipulatedEntitiesInterceptor());
            }
        );

        services.AddScoped<Database>();
        services.AddScoped<IUowDatabase>(x => x.GetRequiredService<Database>());
        services.AddScoped<IDatabase>(x => x.GetRequiredService<Database>());
        services.AddScoped<IReadonlyDatabase>(x => x.GetRequiredService<Database>());

        //services.AddScoped<IDbInitializerService, DbInitializerService>();

        return services;
    }


    

}