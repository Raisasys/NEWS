using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CommandBus
{
    public static async Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
	{
        var handler = Context.Current.GetService<ICommandHandler<TCommand, TResponse>>();
        return await handler.Handle(command, cancellationToken);
    }


    public static async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        var handler = Context.Current.GetService<ICommandHandler<TCommand>>();
        await handler.Handle(command, cancellationToken);
    }


    public static void InstallCommandBus<T>(this IServiceCollection services)
    {
        var commandHandlerImplTypes = typeof(T).Assembly.GetTypes()
            .Where(i => 
                i.IsClass && 
                !i.IsAbstract && 
                i.BaseType is { IsGenericType: false } && 
                i.BaseType == typeof(CommandHandlerBase)).ToList();

        foreach (var commandHandlerImplType in commandHandlerImplTypes)
        {
            var commandWithResponseHandlerServiceTypes = commandHandlerImplType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)).ToList();

            foreach (var commandHandlerServiceType in commandWithResponseHandlerServiceTypes)
            {
                services.AddScoped(commandHandlerServiceType, commandHandlerImplType);
            }

            var commandHandlerServiceTypes = commandHandlerImplType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)).ToList();

            foreach (var commandHandlerServiceType in commandHandlerServiceTypes)
            {
                services.AddScoped(commandHandlerServiceType, commandHandlerImplType);
            }
        }
    }
}