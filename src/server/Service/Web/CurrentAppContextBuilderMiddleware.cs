using Microsoft.AspNetCore.Http;

namespace Service.Web;
public class CurrentAppContextBuilderMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentAppContextBuilderMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        await context.BuildCurrentIdentityContext();

        await _next.Invoke(context);

    }

}