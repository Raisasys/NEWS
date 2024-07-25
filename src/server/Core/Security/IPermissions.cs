namespace Core;

public interface IPermission
{
    public string Name { get; }
    IEnumerable<PermissionKey> Keys { get; }
}


public record PermissionKey(string Key)
{
    public string Key { get; set; } = Key;
}
