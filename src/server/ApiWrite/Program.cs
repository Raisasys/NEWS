using Core;

var builder = WebApplication.CreateBuilder(args);
App.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
App.ConfigurePipeline(app);
app.MigrateDatabase();
//app.Initialize();
app.Run();



/*
services.AddMvc(options =>
{
    options.Filters.Add(new ModelStateFilter());
})*/