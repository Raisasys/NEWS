namespace Core;

public interface IAggregate<out TId> : IEntity<TId>, IHaveAggregate { }

