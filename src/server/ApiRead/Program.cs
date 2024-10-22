using Service.Web;

WebApplication
	.CreateBuilder(args)
	.Host<ReadApp>()
    .Initialize()
	.Run();
