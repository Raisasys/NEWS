using System.Linq.Expressions;
using System.Reflection;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public static class GeneralQueryFilters
{
    public static List<Type> entityBaseTypes = new List<Type> { typeof(IntEntity), typeof(GuidEntity), typeof(StringEntity), typeof(Entity<>) };
    
    public static Expression<Func<TEntity, bool>> SoftDeleteFilter<TEntity>() where TEntity : IEntity => x => !x.IsDeleted;

    public static void MapSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        var softDeleteFilterMethod = typeof(GeneralQueryFilters).GetMethod(nameof(SoftDeleteFilter), BindingFlags.Static | BindingFlags.Public);
        var hasQueryFilter = typeof(EntityTypeBuilder).GetMethod(nameof(EntityTypeBuilder.HasQueryFilter));
        var entityTypes = TypeProvider.EntityTypes.Where(t=> entityBaseTypes .Contains(t.BaseType)).ToList();
        foreach (var entityType in entityTypes)
        {
            var entityBuilder = modelBuilder.Entity(entityType);
            var genericSoftDeleteFilterMethod = softDeleteFilterMethod.MakeGenericMethod(entityType);
            var lamd = genericSoftDeleteFilterMethod.Invoke(null, null);
            var args = new[] { lamd };
            hasQueryFilter.Invoke(entityBuilder, args);
        }
    }
    
}