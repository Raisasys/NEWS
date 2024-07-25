namespace Core;

public interface IGuardClause
{
}

public class Guard : IGuardClause
{
    /// <summary>
    /// An entry point to a set of Guard Clauses.
    /// </summary>
    public static IGuardClause Against { get; } = new Guard();

    private Guard() { }
}
