namespace Core;

public interface IIdGenerator<out TId>
{
    TId New();
}
