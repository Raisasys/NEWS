using Core;

namespace Domain;

public class ObjectiveAction : GuidEntity
{
    protected ObjectiveAction() { }

    public ObjectiveAction(Guid id ,string title, int order, DateTime? originalDueDate, Guid whoPersonId, int important, int urgent, int? startYear, Month? startMonth, int? startWeek, int? endYear, Month? endMonth, int? endWeek) : base(id)
    {
        Title = title;
        Order = order;
        OriginalDueDate = originalDueDate;
        Who = new ActionWho(whoPersonId);
        ImportantUrgent = new ImportantUrgent(important, urgent);
        Duration = new ActionDuration(startYear,startMonth, startWeek, endYear,endMonth, endWeek);
    }

    public ObjectiveAction(Guid id,string title, int order, DateTime? originalDueDate, Guid whoPersonId):base(id)
    {
        Title = title;
        Order = order;
        OriginalDueDate = originalDueDate;
        Who = new ActionWho(whoPersonId);
    }

    [IdentifierKey]
    public long Key { get; set; }
    public string Title { get; set; }
    public int Order{ get; set; }
    public ActionWho Who { get; set; }
    
    public virtual ICollection<ActionStatus> Statuses { get; set; } = new List<ActionStatus>();
    [Calculated] public Status PreviousStatus => Statuses.Count > 1 ? Statuses.OrderByDescending(i => i.CreatedAt).ElementAt(1).Status : CurrentStatus;
    [Calculated] public Status CurrentStatus => Statuses.HasAny() ? Statuses.MaxBy(i => i.CreatedAt).Status : null; 

    public DateTime? OriginalDueDate { get; set; }
    public DateTime? NewDueDate { get; set; }
    public ImportantUrgent ImportantUrgent { get; set; }
    public ActionDuration Duration { get; set; }
    public virtual ICollection<ActionComment> Comments { get; set; } = new List<ActionComment>();

    public ActionStatus SetStatus(Direction direction, RAG rag)
    {
        var state = new ActionStatus(new Status(rag, direction));
        Statuses.Add(state);
        return state;
    }

    public ActionComment AddComment(string comment)
    {
        var actionComment = new ActionComment(comment);
        Comments.Add(actionComment);
        return actionComment;
    }
}