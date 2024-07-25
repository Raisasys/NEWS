#if NET8_0
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static partial class XExtensions
{
    public static IServiceCollection Replace<TService, TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

        services.Remove(descriptorToRemove);

        var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

        services.Add(descriptorToAdd);

        return services;
    }
}

#endif