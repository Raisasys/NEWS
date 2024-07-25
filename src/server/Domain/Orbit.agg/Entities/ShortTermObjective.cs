using Core;

namespace Domain;

public class ShortTermObjective : GuidEntity
{
    protected ShortTermObjective() { }

    public ShortTermObjective(Guid id ,string title, /*LongTermObjective longTermObjective,*/ Year year, ObjectiveOwner owner) : base(id)
    {
        Title = title;
        //LongTermObjective = longTermObjective;
        Year = year;
        Owner = owner;
    }

    [IdentifierKey]
    public long Key { get; set; }
    public string Title { get; set; }
    //public virtual LongTermObjective LongTermObjective { get; set; }
    public virtual Year Year { get; set; }
    public ObjectiveOwner Owner { get; set; }
    public Status Status { get; set; }

    public virtual ICollection<MonthlyStatus> MonthlyStatuses { get; set; } = new List<MonthlyStatus>();
    public virtual ICollection<ObjectiveAction> Actions { get; set; } = new List<ObjectiveAction>();

    public MonthlyStatus SetStatus(Month month, Direction direction, RAG rag)
    {
        var state = new MonthlyStatus(month, new Status(rag, direction));
        MonthlyStatuses.Add(state);
        return state;
    }

    public void AddAction(Guid id ,string title, int order, DateTime? originalDueDate, Guid whoPersonId, int important, int urgent, int? startYear, Month? startMonth, int? startWeek,int? endYear, Month? endMonth, int? endWeek)
    {
        var action = new ObjectiveAction(id,title, order, originalDueDate, whoPersonId, important, urgent, startYear, startMonth, startWeek,endYear , endMonth, endWeek);
        Actions.AddEntity(action);
    }

    public void AddAction(Guid id, string title, int order, DateTime? originalDueDate, Guid whoPersonId)
    {
        var action = new ObjectiveAction(id,title, order, originalDueDate, whoPersonId);
        Actions.AddEntity(action);
    }
}