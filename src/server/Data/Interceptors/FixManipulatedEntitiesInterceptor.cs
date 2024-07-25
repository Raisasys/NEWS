using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Data;

public class FixManipulatedEntitiesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        var currentUserIdentity = Context.Current.GetService<ICurrentAppContext>()?.UserIdentity();
        var currentUserEmail = currentUserIdentity?.User?.Email;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { Entity: IEntity entity }) continue;
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.Created(currentUserEmail);
                    break;
                case EntityState.Modified:
                    entity.Modified(currentUserEmail);
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entity.Deleted(currentUserEmail);
                    break;
                default: break;
            }
        }
        return result;
    }
}