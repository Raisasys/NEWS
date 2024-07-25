
namespace Core;

public abstract class CommandHandlerBase
{
    protected IUowDatabase Database => Context.GetDatabase();
    protected ICurrentAppContext CurrentAppContext => Context.Current.GetService<ICurrentAppContext>();
}