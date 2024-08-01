using Core;
using Service.Web;

WebApplication
	.CreateBuilder(args)
	.Host<WriteApp>()
	.MigrateDatabase()
	.Run();
