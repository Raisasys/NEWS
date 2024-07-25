using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Service;

public static class InitializerActivator
{
    public static void Initialize(this IServiceScope scope, params Initializer[] initializers)
    {
        try
        {
            Context.StartupInitialize(scope.ServiceProvider);

            var rootInitializer = new StartupInitializer();
            Initializer next = rootInitializer;
            foreach (var initializer in initializers)
            {
                next = next.Next(initializer);
            }
            /*rootInitializer
                .Next(new DatabaseInitializer())
                .Next(new EnumInitializer())
                //.Next(//..Go on)
                ;*/
            rootInitializer.Initialize().GetAwaiter().GetResult();

            Context.DisposeStartupInitialize();
        }
        catch (Exception ex)
        {
            //Log errors or do anything you think it's needed
            throw;
        }
    }
}