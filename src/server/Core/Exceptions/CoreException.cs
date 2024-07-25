namespace Core;

public class CoreException : Exception
{
    public CoreExceptionType ExceptionType { get; set; }

    public CoreException(CoreExceptionType exceptionType) : base(exceptionType.ToString()) { ExceptionType = exceptionType; }
    public CoreException(string message) : base(message) { ExceptionType = CoreExceptionType.CustomMessage; }
    public CoreException(string message, Exception innerException) : base(message, innerException) { ExceptionType = CoreExceptionType.CustomMessage; }
}

public enum CoreExceptionType
{
    Deleted, WrongType, AutoGeneratingKeyType, EmptyKey, WrongResponse, CustomMessage
}