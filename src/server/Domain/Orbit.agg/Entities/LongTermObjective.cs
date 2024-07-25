using Core;

namespace Domain;

public class LongTermObjective : GuidEntity
{
    protected LongTermObjective() { }

    public LongTermObjective(Guid id ,string title, int order):base(id)
    {
        Title = title;
        Order = order;
    }
    
    [IdentifierKey]
    public long Key { get; set; }
    public string Title{ get; set; }
    public int Order { get; set; }
    public virtual ICollection<ShortTermObjective> ShortTermObjectives { get; set; } = new List<ShortTermObjective>();

    public ShortTermObjective AddShortTermObjective(Guid id ,Year year, string title, Guid ownerPersonId)
    {
        var shortObjective = new ShortTermObjective(id,title, year, new ObjectiveOwner(ownerPersonId));
        ShortTermObjectives.AddEntity(shortObjective);
        return shortObjective;
    }
}