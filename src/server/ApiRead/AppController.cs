using Core;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Web;

namespace ApiRead;

[ApiController]
[Route("News/[controller]/[action]")]
public abstract class AppController : AppControllerBase
{
    protected IDatabase Database => Context.GetDatabase();

}
