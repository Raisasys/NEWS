using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class MigrationManager
{
    public static void MigrateDatabase(this IServiceScope scope)
    {
        using var appContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        try
        {
            var migrator = appContext.GetInfrastructure().GetService<IMigrator>();
            var migrations = appContext.Database.GetPendingMigrations();
            appContext.Database.Migrate();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}