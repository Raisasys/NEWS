using System.Linq.Expressions;

namespace Core;

public interface IReadonlyDatabase: IDisposable
{
    IQueryable<TEntity> Set<TEntity>() where TEntity : Entity;
    //TEntity Find<TEntity>(params object[] keyValues) where TEntity : Entity;
    ValueTask<TEntity> Find<TEntity>(params object[] keyValues) where TEntity : Entity;
    ValueTask<TEntity> Find<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : Entity;
    Task<TEntity> SingleOrDefault<TEntity>(Func<TEntity, bool> func) where TEntity : Entity;
    Task<TEntity> FirstOrDefault<TEntity>(Func<TEntity, bool> func = null) where TEntity : Entity;
    Task<bool> Any<TEntity>() where TEntity : Entity;
    Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;
    Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;
    Task<long> LongCount<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;
    Task<TResult> Max<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default) where TEntity : Entity;
    Task<TResult> Min<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default) where TEntity : Entity;

}


public interface IDatabase : IReadonlyDatabase
{
    TEntity Add<TEntity>(TEntity entity) where TEntity : Entity;
    TEntity SetAddEntry<TEntity>(TEntity entity) where TEntity : Entity;
    TEntity Attach<TEntity>(TEntity entity) where TEntity : Entity;
    void Remove<TEntity>(TEntity entity) where TEntity : Entity;
    TEntity Update<TEntity>(TEntity entity) where TEntity : Entity;
    void AddRange<TEntity>(params TEntity[] entities) where TEntity : Entity;
    void AttachRange<TEntity>(params TEntity[] entities) where TEntity : Entity;
    void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : Entity;
    void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : Entity;
    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
    void AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
    void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;

}


public interface IUowDatabase : IDatabase
{
    Task<int> SaveChanges(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new());
    Task<int> SaveChanges(CancellationToken cancellationToken = new());
}