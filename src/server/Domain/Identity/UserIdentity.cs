using Core;

namespace Domain;

public class UserIdentity(UserValue user, RolePermissions rolePermissions) : IUserIdentity
{
    public UserValue User { get; } = user;
    public RolePermissions RolePermissions { get; } = rolePermissions;
}