using Core;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiRead;


public static class OpenApiConfigureExtensions
{
    public static void AddSwaggerOpenApi<TStartup>(this IServiceCollection services)
    {
        //var xmlWebsitePath = Path.Combine(AppContext.BaseDirectory, "Website.xml");
        //var xmlDomainPath = Path.Combine(AppContext.BaseDirectory, "Domain.xml");
        //var xmlEndpointsPath = Path.Combine(AppContext.BaseDirectory, "Endpoints.xml");

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(ApiSpec.Version, new OpenApiInfo { Title = ApiSpec.Name, Version = ApiSpec.Version });
            c.CustomOperationIds((apiDesc) => $"x__{apiDesc.ActionDescriptor.RouteValues["controller"]}__{apiDesc.ActionDescriptor.RouteValues["action"]}");
            c.CustomSchemaIds(x => x.FullName);
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}_{apiDesc.ActionDescriptor.RouteValues["action"]}");
            c.AddSwaggerFilter();
            //c.IncludeXmlComments(xmlWebsitePath);
            //c.IncludeXmlComments(xmlDomainPath);
            //c.IncludeXmlComments(xmlEndpointsPath);
            c.ResolveConflictingActions(items =>
            {
                return new ApiDescription();
            });

            c.MapType<IFormFile>(() => new OpenApiSchema
            {
                Title = "File",
                Type = "file",
                Description = "multipart/form-data",
                Format = "FileBody"
            });

        });
        services.AddSwaggerGenNewtonsoftSupport();
        //services.AddSwaggerExamplesFromAssemblyOf(typeof(PostApiResponseModelExample<>));
        //services.AddSwaggerExamples();

        /*typeof(ExamplesPostAction).Assembly
            .GetTypes()
            .Where(i => i.IsClass && !i.IsAbstract && i.BaseType != null && i.BaseType == typeof(ExamplesPostAction))
            .Do(tf =>
            {
                var converterType = typeof(PostApiResponseModelExample<>).MakeGenericType(tf);
                services.AddSingleton(converterType);
            });*/
        //services.AddSingleton<RequestExample>();
        //services.AddSingleton<ResponseExample>();
    }

    public static void UseSwaggerOpenApi(this IApplicationBuilder app)
    {
        //app.UseMiddleware<EnableSwaggerModeInRequestAppContextMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(ApiSpec.ApiSpecEndpoint, ApiSpec.Name);
            c.RoutePrefix = string.Empty;
            //c.RoutePrefix = "docs";
            //c.RoutePrefix = "api-docs";
            //c.SwaggerEndpoint($"/api-docs/{version}/spec.json", $"{name} - v{version}");
        });
    }



    public static void AddSwaggerFilter(this SwaggerGenOptions options)
    {
        // [SwaggerRequestExample] & [SwaggerResponseExample]
        // version < 3.0 like this: c.OperationFilter<ExamplesOperationFilter>(); 
        // version 3.0 like this: c.AddSwaggerExamples(services.BuildServiceProvider());
        // version > 4.0 like this:
        //swaggerGenOptions.OperationFilterDescriptors.Add(new FilterDescriptor

        /*typeof(ExamplesPostAction).Assembly
            .GetTypes()
            .Where(i => i.IsClass && !i.IsAbstract && i.BaseType!= null && i.BaseType == typeof(ExamplesPostAction))
            .Do(tf =>
            {
                var converterType = typeof(PostApiResponseModelExample<>).MakeGenericType(tf);
                options..Add(new FilterDescriptor
                {
                    Type = converterType,
                });
            });*/

        //options.OperationFilter<SwaggerFilter.ProjectHeaderSwaggerAttribute>();
        //options.OperationFilter<SwaggerFileOperationFilter>();
        //options.OperationFilter<FormFileOperationFilter>();
        //options.SchemaFilter<SwaggerFilter.HideFromHeaderPropertySchemaFilter>();
        //options.SchemaFilter<HideBlobTypePropertySchemaFilter>();


        //options.ExampleFilters();
        //options.EnableAnnotations();
        //options.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); 
        //options.OperationFilter<AddHeaderOperationFilter>();
        //options.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]

        //options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        // options.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();

        //options.OperationFilter<SecurityRequirementsOperationFilter>();
        // options.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();

        // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = @"Standard Authorization header using the Bearer scheme. Example: ""bearer {token}""",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
    }

    public static void MapSwaggerEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapSwagger(pattern: "/api-docs/{documentName}/spec.json");
    }

    public static void AddSwaggerApiExplorer(this IMvcCoreBuilder builder)
    {
        builder.AddApiExplorer();
    }

    public static bool IsFromForm(this ApiParameterDescription apiParameter)
    {
        var source = apiParameter.Source;
        var elementType = apiParameter.ModelMetadata?.ElementType;

        return (source == BindingSource.Form || source == BindingSource.FormFile)
               || (elementType != null && typeof(IFormFile).IsAssignableFrom(elementType));
    }
}


public static class ApiSpec
{
    public static string Version = $"v{Config.Get("Me:Version")}";
    public static string Name = Config.Get("Me:Name");
    public static string ApiSpecEndpoint = $"api-docs/{Version}/spec.json";

    public static List<string> EndpointAssemblies = new List<string> { typeof(ApiSpec).Assembly.FullName, typeof(AppController).Assembly.FullName };
}

