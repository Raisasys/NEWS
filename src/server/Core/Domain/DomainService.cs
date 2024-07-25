namespace Core;

public abstract class DomainService
{
    protected IDatabase Database => Context.GetDatabase();
    protected ICurrentAppContext CurrentAppContext => Context.Current.GetService<ICurrentAppContext>();

}