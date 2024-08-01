using ApiRead;
using Domain;
using Domain.ModelMappings;
using Service.Web;
using QueryServices;

class ReadApp : App<ReadApp, News, NewsMapping>
{
	protected override void AddAppControllers()
	{
		Services.AddAppControllersBase<AppController>(
			OpenApiConfigureExtensions.AddSwaggerApiExplorer
		);
	}

	protected override void ConfigureDatabase(IApplicationBuilder app)
	{
		var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
		using var scope = scopeFactory.CreateScope();
		/*var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
        dbInitializer.Initialize();
        dbInitializer.SeedData();*/
	}

	protected override void ConfigureServices()
	{
		Services.AddQueryServices();
	}
}