using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Core;

namespace Data;


public static class InheritanceEntityMapperPipeline
{
    private static readonly IList<IInheritanceEntityMapper> Mappers;

    static InheritanceEntityMapperPipeline()
    {
        Mappers = new List<IInheritanceEntityMapper>();
        SubscribeMappers();
    }

    public static void Subscribe<TMapper>() where TMapper : IInheritanceEntityMapper, new()
        => Mappers.Add(new TMapper());


    public static void InheritanceEntityMap(this ModelBuilder modelBuilder)
    {
        foreach (var mapper in Mappers)
            mapper.Map(modelBuilder);
    }

    private static void SubscribeMappers()
    {
        var subscribeMethod = typeof(InheritanceEntityMapperPipeline).GetMethod(nameof(InheritanceEntityMapperPipeline.Subscribe), BindingFlags.Static | BindingFlags.Public);
        var mappers = TypeProvider.ModelMappingsAssembly.GetTypes().Where(p => p.IsClass && !p.IsAbstract && p.Implements<IInheritanceEntityMapper>()).ToList();
        foreach (var mapper in mappers)
        {
            var genericSubscribeMethod = subscribeMethod.MakeGenericMethod(mapper);
            genericSubscribeMethod.Invoke(null, null);
        }
    }
}

public interface IInheritanceEntityMapper : IEntityMapperBase
{
    void Map(ModelBuilder modelBuilder);
}

public abstract class InheritanceEntityMapper<TEntityBase, TConcreteType1, TConcreteType2> : IInheritanceEntityMapper
    where TEntityBase : Entity
    where TConcreteType1 : TEntityBase
    where TConcreteType2 : TEntityBase
{
    public void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TEntityBase>().UseTpcMappingStrategy();
        modelBuilder.Entity<TConcreteType1>().ToTable(typeof(TConcreteType1).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType2>().ToTable(typeof(TConcreteType2).PluralizeTypeName());
    }
}

public abstract class InheritanceEntityMapper<TEntityBase, TConcreteType1, TConcreteType2, TConcreteType3> : IInheritanceEntityMapper
    where TEntityBase : Entity
    where TConcreteType1 : TEntityBase
    where TConcreteType2 : TEntityBase
    where TConcreteType3 : TEntityBase
{
    public void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TEntityBase>().UseTpcMappingStrategy();
        modelBuilder.Entity<TConcreteType1>().ToTable(typeof(TConcreteType1).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType2>().ToTable(typeof(TConcreteType2).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType3>().ToTable(typeof(TConcreteType3).PluralizeTypeName());
    }
}

public abstract class InheritanceEntityMapper<TEntityBase, TConcreteType1, TConcreteType2, TConcreteType3, TConcreteType4> : IInheritanceEntityMapper
    where TEntityBase : Entity
    where TConcreteType1 : TEntityBase
    where TConcreteType2 : TEntityBase
    where TConcreteType3 : TEntityBase
    where TConcreteType4 : TEntityBase
{
    public void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TEntityBase>().UseTpcMappingStrategy();
        modelBuilder.Entity<TConcreteType1>().ToTable(typeof(TConcreteType1).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType2>().ToTable(typeof(TConcreteType2).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType3>().ToTable(typeof(TConcreteType3).PluralizeTypeName());
        modelBuilder.Entity<TConcreteType4>().ToTable(typeof(TConcreteType4).PluralizeTypeName());
    }
}
