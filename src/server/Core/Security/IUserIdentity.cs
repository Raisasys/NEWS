namespace Core;

public interface IUserIdentity
{
    UserValue User { get; }
    RolePermissions RolePermissions { get; }
}

public interface IUserIdentityService
{
    Task<IUserIdentity> Resolve(string userId);
}