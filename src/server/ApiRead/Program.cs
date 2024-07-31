using Jaguar.Service.Web;

WebApplication
	.CreateBuilder(args)
	.Host<ReadApp>()
	.Run();
