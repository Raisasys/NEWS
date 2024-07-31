using Core;
using Jaguar.Service.Web;

WebApplication
	.CreateBuilder(args)
	.Host<WriteApp>()
	.MigrateDatabase()
	.Run();
