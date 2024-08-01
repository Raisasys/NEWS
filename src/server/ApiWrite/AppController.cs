using Core;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Web;

namespace ApiWrite;

[ApiController]
[Route("[controller]/[action]")]
public abstract class AppController : AppControllerBase
{

    protected IDatabase Database => Context.GetDatabase();
    
}
