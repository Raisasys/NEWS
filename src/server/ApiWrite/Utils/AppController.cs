using Core;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Web;

namespace ApiWrite;

[ApiController]
[Route("[controller]/[action]")]
public abstract class AppController : ControllerBase
{

    [NonAction]
    public bool IsModelInvalid(object model)
    {
        if (model == null || ModelState.IsValid == false)
            return true;
        return false;
    }

    protected ICurrentAppContext CurrentContext => Context.Current.GetService<ICurrentAppContext>();

    protected IUserIdentity Role => CurrentContext.UserIdentity();
    protected IDatabase Database => Context.GetDatabase();
    protected ILogger Log => Context.GetLogger();
    protected IWebHostEnvironment Environment => AppConfig.Env;

    protected HttpContext CurrentHttpContext => Context.Current.Http();
    protected HttpRequest CurrentRequest => Context.Current.Http().Request;
    protected HttpResponse CurrentResponse => Context.Current.Http().Response;
    
}
