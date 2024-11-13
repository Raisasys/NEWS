using Core;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Web;

namespace ApiWrite;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AppController : AppControllerBase
{

    protected IDatabase Database => Context.Current.GetService<IDatabase>();
    
}
