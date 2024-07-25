using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class TypeProvider
{
    public static Assembly EntitiesAssembly;
    public static Assembly ModelMappingsAssembly;
    public static Assembly SeedDataInitializerAssembly;

    public static void SetEntitiesAssembly<TEntity>() where TEntity : IEntity => EntitiesAssembly = typeof(TEntity).Assembly;
    public static void SetModelMappingsAssembly<TMappings>() where TMappings : IEntityMapperBase => ModelMappingsAssembly = typeof(TMappings).Assembly;
    public static void AddSeedDataInitializerAssembly<TSeedDataInitializer>(this IServiceCollection services) where TSeedDataInitializer : ISeedDataInitializerBase => SeedDataInitializerAssembly = typeof(TSeedDataInitializer).Assembly;

    public static IList<Type> SeedDataInitializerTypes => SeedDataInitializerAssembly
        .GetTypes()
        .Where(x => x.IsSubclassOf(typeof(SeedDataInitializer)) && !x.IsAbstract)
        .ToList();

    public static IList<Type> SeedDevDataInitializerTypes => SeedDataInitializerAssembly
        .GetTypes()
        .Where(x => x.IsSubclassOf(typeof(SeedDevDataInitializer)) && !x.IsAbstract)
        .ToList();

    public static IList<Type> EntityTypes => EntitiesAssembly
        .GetTypes()
        .Where(x => x.BaseType != null && !x.IsAbstract && !x.IsGenericType && x.Implements<IEntity>())
        .ToList();


    public static IList<Type> HttpClients<THttpClient>() => typeof(THttpClient).Assembly.GetTypes()
        .Where(x => x.BaseType != null && !x.IsAbstract && !x.IsGenericType && x.Implements<IEntity>())
        .ToList();

    public static List<Type> AppControllers<TControllerBase>() => typeof(TControllerBase).Assembly.GetTypes()
        .Where(x => x.BaseType != null && x.BaseType == typeof(TControllerBase) && !x.IsAbstract && !x.IsGenericType)
        .ToList();

    public static bool AssignableFromGenericType(this Type type, Type baseGenericType)
    {
        if (type.BaseType == null || type.BaseType == typeof(object)) return false;
        return type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == baseGenericType || AssignableFromGenericType(type.BaseType, baseGenericType);
    }

    public static bool WithNotImplement(this Type type, Type baseType)
    {
        if (type == null || type == typeof(object)) return true;
        return !type.Implements(baseType) && type.BaseType.WithNotImplement(baseType);
    }

    public static bool WithImplement(this Type type, Type baseType)
    {
        if (type == null || type == typeof(object)) return false;
        return type.Implements(baseType) || type.BaseType.WithImplement(baseType);
    }

    public static Type GetGenericTypeByAssignableFromGenericType(this Type type, Type baseGenericType)
    {
        if (type.BaseType == null || type.BaseType == typeof(Object)) return null;
        return type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseGenericType ? type.BaseType : GetGenericTypeByAssignableFromGenericType(type.BaseType, baseGenericType);
    }

    
}