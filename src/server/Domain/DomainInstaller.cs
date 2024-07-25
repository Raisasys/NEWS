using Microsoft.Extensions.DependencyInjection;
using Core;
using FluentValidation;

namespace Domain;

public static class DomainInstaller
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<ICompanyDomainService, CompanyDomainService>();
        services.AddScoped<IOrbitDomainService, OrbitDomainService>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        //services.InstallSettingsServices();

    }

}