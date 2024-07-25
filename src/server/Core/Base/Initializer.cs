using Microsoft.Extensions.DependencyInjection;

namespace Core;

public abstract class Initializer
{
    protected Initializer _next { get; set; }

    public Initializer Next(Initializer next)
    {
        if (next is RootInitializer)
            throw new AppLaunchException("Just define only one RootInitializer in Initializer chain");
        _next = next;
        return _next;
    }


    protected async Task initialize()
    {
        await Do();
        if (_next != null)
            await _next.initialize();
    }


    protected abstract Task Do();
}



public abstract class RootInitializer : Initializer
{
    public async Task Initialize() => await initialize();
}

public class StartupInitializer : RootInitializer
{
    protected override async Task Do()
    {
        Console.WriteLine("StartupInitializer run ...");
    }
}
