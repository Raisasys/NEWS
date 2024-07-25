namespace Core;

public interface IEntity : IHaveAudit, IDeletableEntity, IComparable
{
    Task Validate(IReadonlyDatabase database);
    object GetId();
    IEntity Clone();
    
}

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}