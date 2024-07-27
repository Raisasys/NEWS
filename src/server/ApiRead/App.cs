using ApiRead;
using Core;
using Domain;
using Data;
using Domain.ModelMappings;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QueryServices;
using Service;
using Service.Web;

public static class App
{
    public static IConfiguration Configuration { get; private set; }
    public static IWebHostEnvironment Environment { get; private set; }


    public static void Initialize(WebApplication app)
    {
        Configuration = app.Configuration;
        Environment = app.Environment;
        AppConfig.SetConfiguration(Configuration, Environment);
        //Config.SetupAppConfig(new AppConfig());
        AppConfig.MergeEnvironmentVariables(Configuration);

        Context.Initialize(
	        app.Services, 
	        () => app.Services.GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices,
	        null,
	        null);
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        Config.SetupAppConfig(new AppConfig());

        services.AddHttpContextAccessor();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IScopedCache, ScopedCache>();
        services.AddSingleton<IAppConfig, AppConfig>();
		services.AddDistributedCache();
		services.AddCurrentAppContext();
        services.AddAppHttpClient();
        services.AddFileUploadOptions();
        services.AddApplicationServices();
        services.AddQueryServices();
        //services.AddApiProvider(Configuration);
        //services.AddOptionsFromAppSetting<bot_setting>(Configuration, "bot_setting");
        //services.AddScoped<ITokenValidatorConsumerService, TokenValidatorConsumerService>();

        services.AddCors();
		AddAppControllers(services);
        services.AddMvc();

        //builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen();

        //services.AddFluentValidationAutoValidation();

        services.AddDatabase<News, NewsMapping>(typeof(Program).Assembly);
        //services.AddBearerTokensOptions(Configuration);
        //services.AddConsumerAuthenticationAndAuthorizationByJwtBearer(Configuration);
        /*if (Environment.IsProduction())
        {
            services.AddSerilogUi(options => options.UseMongoDb(
                Configuration["Serilog:MongoDb:Url"],
                Configuration["Serilog:MongoDb:Database"],
                Configuration["Serilog:MongoDb:Collection"]));
        }*/
        services.AddSignalR();
        //services.AddAuthServices(configuration);
        services.AddDomainServices();
        services.AddSwaggerOpenApi<AppConfig>();

        Console.WriteLine("Service Provider Configuration Done...");

        return services;
    }


    public static IApplicationBuilder ConfigurePipeline(WebApplication app)
    {
        Initialize(app);

        if (!Environment.IsDevelopment())
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseCorsFromConfig(Configuration);

        app.UseSwaggerOpenApi();

		app.UseMiddleware<AppExceptionHandlerMiddleware>();

		//app.UseAnonymousAuthHandlers();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CurrentAppContextBuilderMiddleware>();

        //app.UseIdentityAuthHandlers();

        //UseXMiddleware(app, env, loggerFactory);
        app.UseAppRouting();
        app.UseFileUpload();
        
        UseAppEndpoints(app);
        
        /*if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }*/


        //app.MapControllers();


        //app.UseSerilogRequestLogging();
        //if (env.IsProduction())
        //app.UseSerilogUi(option => { option.RoutePrefix = "logs"; });

        ConfigureDatabase(app);

        Console.WriteLine("Pipeline Configuration Done...");
        Console.WriteLine($"Listening on " + Config.Get("Me:Url"));

        return app;
    }
    
    public static void ConfigureDatabase(IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        //var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
        //dbInitializer.Initialize();
        //dbInitializer.SeedData();
    }
    
    public static void AddAppControllers(IServiceCollection services)
    {
        services.AddAppControllersBase<AppController>(
            OpenApiConfigureExtensions.AddSwaggerApiExplorer
        );
    }

    public static IApplicationBuilder UseAppEndpoints(IApplicationBuilder app)
    {
        app.UseAppEndpointBase("api",
            //AttachHub,
            OpenApiConfigureExtensions.MapSwaggerEndpoint
        );
        return app;
    }

}