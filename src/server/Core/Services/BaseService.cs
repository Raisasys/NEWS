namespace Core.Services;


public abstract class BaseService
{

    protected IDatabase Database => Context.GetDatabase();
}