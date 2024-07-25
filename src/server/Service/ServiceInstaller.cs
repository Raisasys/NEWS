using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Service;

public static class ServiceInstaller
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddSingleton<IEmailProvider, GoogleSendMailProvider>();
        //services.AddScoped<ISmsProvider, ClickSendSmsProvider>();
        //services.AddSingleton<IRequestProcessingService, RequestProcessingService>();

        return services;
    }

    public static IServiceCollection AddCurrentAppContext(this IServiceCollection services)
    {
        services.AddScoped<CurrentAppContext>();
        services.AddScoped<ICurrentAppContext>(x => x.GetRequiredService<CurrentAppContext>());
        return services;
    }
}