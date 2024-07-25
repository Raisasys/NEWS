using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service.Web;
using System.ComponentModel;

namespace Service;

public class AppConfig: IAppConfig
{
    private static IConfiguration _configuration;
    private static IWebHostEnvironment _environment;

    private static IConfiguration Configuration => _configuration ??= Context.Current.Config;
    public static IWebHostEnvironment Env => _environment;


    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void SetConfiguration(IConfiguration config, IWebHostEnvironment environment)
    {
        _configuration = config;
        _environment = environment;
    }


    public static IConfigurationSection GetSection(string key)
        => Configuration.GetSection(key);

    public string Get(string key) => Get(key, string.Empty);

    public string GetOrThrow(string key)
    {
        var result = Get(key);

        if (result.HasValue()) return result;
        else throw new($"AppSetting value of '{key}' is not specified.");
    }

    public string Get(string key, string defaultValue) => Configuration.GetValue(key, defaultValue);

    public T Get<T>(string key) => Get<T>(key, default);

    public T Get<T>(string key, T defaultValue) => Configuration.GetValue(key, defaultValue);

    public string GetConnectionString(string name = "MainDatabase") => _configuration.GetConnectionString(name);

    public bool IsDevelopment => _environment.IsDevelopment();
    public bool IsProduction => _environment.IsProduction();


    public static Dictionary<string, string> GetSubsection(string sectionKey, bool relativeKeys)
    {
        var settings = GetSection(sectionKey)?.AsEnumerable(relativeKeys) ?? Enumerable.Empty<KeyValuePair<string, string>>();

        return settings.ToDictionary(x => x.Key, x => x.Value);
    }

    
    public static IConfiguration MergeEnvironmentVariables(IConfiguration config)
    {
        var keys = Environment.GetEnvironmentVariables().Keys.Cast<string>().ToArray();

        foreach (var variable in keys)
        {
            var key = $"%{variable}%";
            var configNodes = config.AsEnumerable().Where(v => v.Value.HasValue() && v.Value.Contains(key)).ToArray();

            foreach (var item in configNodes)
            {
                var value = GetSafeEnvironmentVariable(variable);
                var finalValue = item.Value.Replace(key, value);

                try
                {
                    config[item.Key] = finalValue;
                }
                catch
                {
                    Console.WriteLine("Failed to update config key from environment variable.");
                }
            }
        }

        return config;
    }

    public static string GetSafeEnvironmentVariable(string key)
    {
        return new[] { "\\n", "\\r", "\\r\\n" }.Aggregate(
            Environment.GetEnvironmentVariable(key),
            (v, t) => v.Replace(t, Environment.NewLine)
        );
    }
}

