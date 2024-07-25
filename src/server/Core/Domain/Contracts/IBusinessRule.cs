namespace Core;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}
