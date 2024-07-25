namespace Core;

public class RolePermissions
{
    public RolePermissions(Role role, IEnumerable<IPermission> permissions)
    {
        Role = role;
        Permissions = permissions;
        _permissionKeys = [..permissions.SelectMany(i => i.Keys).ToList()];
    }

    public Role Role{ get; init; }
    public IEnumerable<IPermission> Permissions { get; init; }
    private HashSet<PermissionKey> _permissionKeys { get; init; }
    public IEnumerable<PermissionKey> PermissionKeys => _permissionKeys;
}