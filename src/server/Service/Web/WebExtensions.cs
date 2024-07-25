using System.Security.Claims;
using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Service.Web;

public static partial class WebExtensions
{
    public static async Task BuildCurrentIdentityContext(this HttpContext context)
    {
        var userPrincipal = context.User;
        if (userPrincipal.Identity is not { IsAuthenticated: true }) return;

        var userId = userPrincipal.FindFirst(ClaimTypes.UserData);
        if (userId == null || userId.Value.IsEmpty())
        {
            if (Config.IsDevelopment)
            {
                await Context.Current.GetService<IUserIdentityService>().Resolve("admin@orbit.info");
            }
        }

        await Context.Current.GetService<IUserIdentityService>().Resolve(userId.Value);
    }

    
}


