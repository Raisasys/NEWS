using Service.Web;

WebApplication
	.CreateBuilder(args)
	.Host<ReadApp>()
	.Run();
