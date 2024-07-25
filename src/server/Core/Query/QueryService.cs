
namespace Core;


public abstract class QueryService
{
    protected IReadonlyDatabase Database => Context.GetDatabase();
    protected DateTimeOffset Now { get; } = DateTime.Now;

    protected ICurrentAppContext CurrentAppContext => Context.Current.GetService<ICurrentAppContext>();
}

