using Core;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Web;

partial class WebExtensions
{
    public static IServiceCollection AddAppHttpClient(this IServiceCollection services)
    {
        services.AddTransient<HttpClientMessageHandler>();

        services
            .AddHttpClient("App HttpClient", c => { c.Timeout = 3.Minutes(); })
            .AddHttpMessageHandler<HttpClientMessageHandler>();

        /*services
	        .AddHttpClient(ClickSendSmsProvider.HttpClientName, c => { c.Timeout = 1.Minutes(); })
	        .AddHttpMessageHandler<HttpClientMessageHandler>();*/

		return services;
        /*foreach (var apiClientType in TypeProvider.HttpClients<Program>())
        {
            var apiBaseAddressAttribute = apiClientType.IsDefined<ApiBaseAddressAttribute>();
            if (apiBaseAddressAttribute == null) throw new XException($"ApiBaseAddressAttribute not set for {apiClientType.Name}");

            services
                .AddRefitClient(apiClientType, new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(),

                })
                .ConfigureHttpClient(c =>
                {
                    c.Timeout = 60.Minutes();
                    c.BaseAddress = new Uri(XConfig.Get(apiBaseAddressAttribute.AddressSettingName));
                });

        }*/
    }

    public static IServiceCollection AddOptionsFromAppSetting<TOptions>(this IServiceCollection services, IConfiguration configuration, string appSettingJsonName)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(appSettingJsonName));
        return services;
    }

    public static IServiceCollection AddFileUploadOptions(this IServiceCollection services)
    {
        services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });
        return services;
    }

    public static IServiceCollection AddDistributedCache(this IServiceCollection services)
    {
	    services.AddStackExchangeRedisCache(options =>
	    {
		    options.Configuration = Config.Get("Redis:ConnectionString") ?? "localhost:6379";
	    });
	    return services;
    }


}

