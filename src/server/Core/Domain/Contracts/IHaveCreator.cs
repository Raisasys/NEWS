namespace Core;

public interface IHaveCreator
{
    DateTimeOffset CreatedAt { get; protected set; }
    string CreatedBy { get; protected set; }

    public void Created(string createdBy)
    {
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
    }
}
