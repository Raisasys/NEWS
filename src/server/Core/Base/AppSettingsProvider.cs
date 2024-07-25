using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class AppSettingsProvider
{
    private static IAppSettings AppSettings { get; set; }

    public static void RefreshAppSettings(IAppSettings newAppSettings)
    {
        var propertyTypes = typeof(IAppSettings).GetProperties().Where(p=> !p.IsDefined(typeof(CalculatedAttribute),false)).ToList();

        foreach (var propertyType in propertyTypes)
        {
            var newValue = propertyType.GetValue(newAppSettings);
            propertyType.SetValue(AppSettings, newValue);
        }
    }


    /*public static void InstallSettingsServices(this IServiceCollection services)
    {
        var settingsServiceTypes = typeof(AppSettings).GetInterfaces().ToList();
        foreach (var settingsServiceType in settingsServiceTypes)
            services.AddSingleton(settingsServiceType, AppSettings);


    }*/
}
