namespace Core;

public interface IHaveAudit : IHaveCreator
{
    DateTimeOffset? LastModifiedAt { get; protected set; }
    string LastModifiedBy { get; protected set; }

    public void Modified(string modifiedBy)
    {
        LastModifiedAt = DateTimeOffset.UtcNow;
        LastModifiedBy = modifiedBy;
    }
}

public interface IDeletableEntity
{
    bool IsDeleted { get; protected set; }
    DateTimeOffset? DeletedAt { get; protected set; }
    string DeletedBy { get; protected set; }

    public void Deleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
        DeletedBy = deletedBy;
    }
    public void Recover()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }
}
