using System;

namespace Core;

public static class Config
{
    private static IAppConfig _appConfig;

    public static void SetupAppConfig(IAppConfig config) => _appConfig = config;

    public static string Get(string key) => Get(key, string.Empty);

    public static string GetOrThrow(string key)
    {
        var result = Get(key);

        if (result.HasValue()) return result;
        else throw new($"AppSetting value of '{key}' is not specified.");
    }

    public static string Get(string key, string defaultValue) => _appConfig.Get(key,defaultValue);

    public static T Get<T>(string key) => Get<T>(key, default);

    public static T Get<T>(string key, T defaultValue) => _appConfig.Get<T>(key, defaultValue);

    public static string GetConnectionString(string name = "MainDatabase") => _appConfig.GetConnectionString(name);

    public static bool IsDevelopment => _appConfig.IsDevelopment;
    public static bool IsProduction => _appConfig.IsProduction;
}


public interface IAppConfig
{
    string Get(string key);
    string GetOrThrow(string key);
    string Get(string key, string defaultValue);
    T Get<T>(string key);
    T Get<T>(string key, T defaultValue);
    string GetConnectionString(string name = "MainDatabase");
    bool IsDevelopment { get; }
    bool IsProduction { get; }
}