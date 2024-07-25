using System;
using System.Collections.Generic;
using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Service.Web;


partial class WebExtensions
{
	static WebExtensions()
	{
		JsonSerializerSettings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			DateFormatString = "yyyy-MM-ddTHH:mm:ss" //"yyyy-MM-ddTHH:mm:ss.fffZ";
		};
		JsonSerializerSettings.Converters = JsonSerializerSettings.Converters ?? new List<JsonConverter>();
		JsonSerializerSettings.Converters.Add(new AppEnumConverter());
        JsonSerializerSettings.Converters.Add(new AppDateTimeConverter());
    }

	public static JsonSerializerSettings JsonSerializerSettings { get; }

	public static void ConfigureNewtonsoftJsonOptions(MvcNewtonsoftJsonOptions options)
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new AppEnumConverter());
        options.SerializerSettings.Converters.Add(new AppDateTimeConverter());
        options.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        //options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss"; //"yyyy-MM-ddTHH:mm:ss.fffZ";
    }



    public static void AddAppControllersBase<TControllerBase>(this IServiceCollection services, params Action<IMvcCoreBuilder>[] extraBuilderActions)
    {
        var mvcCoreBuilder = services
            .AddMvcCore(options => { options.EnableEndpointRouting = false; })
            .AddAppControllers<TControllerBase>()
            .ConfigureApiBehaviorOptions(options => { })
            .AddNewtonsoftJson(ConfigureNewtonsoftJsonOptions);

        extraBuilderActions?.Do(action => action(mvcCoreBuilder));

        //services.AddResponseCompression();
        //services.AddResponseCaching();
    }
    
    public static IApplicationBuilder UseAppEndpointBase(this IApplicationBuilder app,string name, params Action<IEndpointRouteBuilder>[] extraMapActions)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(name: name, pattern: "{controller}/{action}/{id?}");
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"Hello, I'm {name} ...");
            });
            extraMapActions?.Do(action => action(endpoints));
        });
        return app;
    }

    public static IApplicationBuilder UseWorkerEndpoint(this IApplicationBuilder app, string name, params Action<IEndpointRouteBuilder>[] extraMapActions)
    {
	    app.UseEndpoints(endpoints =>
	    {
		    endpoints.MapGet("/", async context =>
		    {
			    await context.Response.WriteAsync($"Hello, I'm {name} ...");
		    });
		    extraMapActions?.Do(action => action(endpoints));
	    });
	    return app;
    }

	public static IApplicationBuilder UseAppRouting(this IApplicationBuilder app)
    {
        app.UseRouting();
        return app;
    }



    /*
    public static void AttachHub(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var hubs = XTypeProvider.XHubTypes;

        foreach (var hub in hubs)
        {
            var hubInstance = Activator.CreateInstance(hub);
            var attachHubMethod = hub.GetMethod("AttachHub");
            attachHubMethod.Invoke(hubInstance, new[] { endpointRouteBuilder });
        }
    }*/
}