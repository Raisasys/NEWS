using Microsoft.Extensions.DependencyInjection;
using Core;


namespace Domain;

public static class DomainInstaller
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<INewsDomainService, NewsDomainService>();
     
    }
}