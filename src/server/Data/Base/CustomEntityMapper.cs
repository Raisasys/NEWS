using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core;

namespace Data;

public static class CustomEntityMapperPipeline
{
    private static readonly IList<ICustomEntityMapper> Mappers;

    static CustomEntityMapperPipeline()
    {
        Mappers = new List<ICustomEntityMapper>();
        SubscribeMappers();
    }

    public static void Subscribe<TMapper>() where TMapper : ICustomEntityMapper, new()
        => Mappers.Add(new TMapper());


    public static void CustomEntityMap(this ModelBuilder modelBuilder)
    {
        foreach (var mapper in Mappers)
            mapper.Map(modelBuilder);
    }

    private static void SubscribeMappers()
    {
        var subscribeMethod = typeof(CustomEntityMapperPipeline).GetMethod(nameof(CustomEntityMapperPipeline.Subscribe), BindingFlags.Static | BindingFlags.Public);
        var mappers = TypeProvider.ModelMappingsAssembly.GetTypes().Where(p => p.IsClass && !p.IsAbstract && !p.IsGenericType && p.Implements<ICustomEntityMapper>()).ToList();
        foreach (var mapper in mappers)
        {
            var genericSubscribeMethod = subscribeMethod.MakeGenericMethod(mapper);
            genericSubscribeMethod.Invoke(null, null);
        }
    }
}


public interface ICustomEntityMapper : IEntityMapperBase
{
    void Map(ModelBuilder modelBuilder);
}

public abstract class CustomEntityMapper<TEntity> : ICustomEntityMapper where TEntity : Entity
{
    public void Map(ModelBuilder modelBuilder)
        => MapBuilder(modelBuilder.Entity<TEntity>());

    public abstract void MapBuilder(EntityTypeBuilder<TEntity> entityBuilder);
}

