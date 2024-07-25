namespace Core;

public class DomainException : CoreException
{
    public DomainException(string message) : base(message)
    {
        Messages = new List<string> { message };
    }
    public DomainException(params string[] messages) : base(messages.ToString(", "))
    {
        Messages = messages;
    }

    public IList<string> Messages{ get; }
    
}