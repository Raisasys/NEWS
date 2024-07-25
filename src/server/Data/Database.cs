using System.Linq.Expressions;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal class Database : IUowDatabase
{
    private readonly DataContext _context;

    public Database()
    {
        _context = Context.Current.GetService<DataContext>();
    }


    public void Dispose()
    {
        _context?.Dispose();
    }

    private DbSet<TEntity> S<TEntity>() where TEntity : Entity => _context.Set<TEntity>();

    public IQueryable<TEntity> Set<TEntity>() where TEntity : Entity =>
        S<TEntity>().AsQueryable();


    public ValueTask<TEntity> Find<TEntity>(params object[] keyValues) where TEntity : Entity
        => S<TEntity>().FindAsync(keyValues);

    public ValueTask<TEntity> Find<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : Entity
        => S<TEntity>().FindAsync(keyValues, cancellationToken);

    public Task<TResult> Max<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default) where TEntity : Entity
        => S<TEntity>().MaxAsync(selector, cancellationToken);


    public Task<TResult> Min<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default) where TEntity : Entity
        => S<TEntity>().MinAsync(selector, cancellationToken);


    public TEntity Add<TEntity>(TEntity entity) where TEntity : Entity
        => S<TEntity>().Add(entity).Entity;

    public TEntity SetAddEntry<TEntity>(TEntity entity) where TEntity : Entity
    {
        _context.Entry(entity).State = EntityState.Added;
        return entity;
    }

    public TEntity Attach<TEntity>(TEntity entity) where TEntity : Entity
        => S<TEntity>().Attach(entity).Entity;

    public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        => S<TEntity>().Remove(entity);

    public TEntity Update<TEntity>(TEntity entity) where TEntity : Entity
        => S<TEntity>().Update(entity).Entity;

    public void AddRange<TEntity>(params TEntity[] entities) where TEntity : Entity
        => S<TEntity>().AddRange(entities);

    public void AttachRange<TEntity>(params TEntity[] entities) where TEntity : Entity
        => S<TEntity>().AttachRange(entities);

    public void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : Entity
        => S<TEntity>().RemoveRange(entities);

    public void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : Entity
        => S<TEntity>().UpdateRange(entities);

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        => S<TEntity>().AddRange(entities);

    public void AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        => S<TEntity>().AttachRange(entities);

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        => S<TEntity>().RemoveRange(entities);

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        => S<TEntity>().UpdateRange(entities);

    public async Task<int> SaveChanges(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    

    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task<TEntity> SingleOrDefault<TEntity>(Func<TEntity, bool> func) where TEntity : Entity
    => await S<TEntity>().Where(func).AsQueryable().SingleOrDefaultAsync();

    public async Task<TEntity> FirstOrDefault<TEntity>(Func<TEntity, bool> func = null) where TEntity : Entity
=> await S<TEntity>().Where(func).AsQueryable().FirstOrDefaultAsync();

    public async Task<bool> Any<TEntity>() where TEntity : Entity
        => await S<TEntity>().AnyAsync();

    public async Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
    => await S<TEntity>().AnyAsync(predicate);

    public async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
        => await S<TEntity>().CountAsync(predicate);


    public async Task<long> LongCount<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
    => await S<TEntity>().LongCountAsync(predicate);
}