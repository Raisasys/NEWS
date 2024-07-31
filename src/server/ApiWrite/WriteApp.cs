using ApiWrite;
using Core;
using Domain;
using Data;
using Domain.ModelMappings;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Service;
using Service.Web;
using Jaguar.Service.Web;

class WriteApp: App<WriteApp,News,NewsMapping>
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
		Services.AddDomainServices();
		Services.AddCommandHandlers();
	}
}
