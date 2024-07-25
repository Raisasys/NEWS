namespace Core;

public static class EntityCollection
{
    public static void AddEntity<TEntity>(this ICollection<TEntity> items, TEntity item) where TEntity : Entity
    {
        Context.Current.GetService<IDatabase>().SetAddEntry(item);
        items.Add(item);
    }
}