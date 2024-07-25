namespace Core;

public class AppLaunchException: CoreException
{
    public AppLaunchException(string message) : base(message)
    {
        Messages = new List<string> { message };
    }
    public AppLaunchException(params string[] messages) : base(messages.ToString(", "))
    {
        Messages = messages;
    }

    public IList<string> Messages { get; }

}