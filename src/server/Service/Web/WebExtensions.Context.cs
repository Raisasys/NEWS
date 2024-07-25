using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Service.Web;
partial class WebExtensions
{
    public static HttpContext Http(this Context context) => context.GetService<IHttpContextAccessor>()?.HttpContext;

    public static ActionContext ActionContext(this Context context) => context.GetService<IActionContextAccessor>()?.ActionContext;

    public static HttpRequest Request(this Context context) => context.Http()?.Request;

    public static HttpResponse Response(this Context context) => context.Http()?.Response;

    

    static IWebHostEnvironment environment;
    public static IWebHostEnvironment GetEnvironment(this Context current) => current.GetService<IWebHostEnvironment>();
    public static IWebHostEnvironment Environment(this Context context) => environment ??= context.GetService<IWebHostEnvironment>();
    public static bool IsDevelopment(this Context current) => current.Environment().IsDevelopment();
    public static bool IsProduction(this Context current) => current.Environment().IsProduction();
}