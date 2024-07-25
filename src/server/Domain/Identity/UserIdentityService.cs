using Core;

namespace Domain;

internal class UserIdentityService : IUserIdentityService
{
    public async Task<IUserIdentity> Resolve(string userId)
    {
        var user = await Context.GetDatabase().Find<User>(userId);
        //if (user == null || user.Disabled) return;
        if (user == null) return null;
        var userIdentity = new UserIdentity(new UserValue(user.Id, user.FullName), null);
        Context.OnCurrent<IUserIdentity>.Set(userIdentity);
        return userIdentity;
    }
}