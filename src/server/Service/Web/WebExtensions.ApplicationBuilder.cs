using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Service.Web;

partial class WebExtensions
{
    public static void UseCorsFromConfig(this IApplicationBuilder app, IConfiguration config)
    {
        var allowed = config["Cors:Allow"].ToLowerOrEmpty().Split(",").Trim().Distinct().ToArray();
        if (allowed.None() || (allowed.Length == 1 && allowed.First() == "*"))
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
        }
        else
        {
            app.UseCors(x => x
                .WithOrigins(allowed)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("Access-Control-Allow-Origin"));
        }
    }


    public static void UseFileUpload(this IApplicationBuilder app)
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), "AppFiles");
        if (!Directory.Exists(tempDirectory)) Directory.CreateDirectory(tempDirectory);
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(tempDirectory)
        });
    }
}