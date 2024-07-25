using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityCreating(modelBuilder);
    }

    private void EntityCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.PropertiesMap();
        modelBuilder.InheritanceEntityMap();
        modelBuilder.CustomEntityMap();
        modelBuilder.MapSoftDeleteQueryFilter();

    }
}