namespace Core;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}


public interface ICommand : IRequest
{
    
}

public abstract class CommandBase
{
    public Guid CommandId { get; init; } = Guid.NewGuid();
    protected ICurrentAppContext CurrentAppContext => Context.Current.GetService<ICurrentAppContext>();
}

public abstract class Command : CommandBase, ICommand
{

}

public abstract class Command<TResponse> : CommandBase, ICommand<TResponse>
{

}