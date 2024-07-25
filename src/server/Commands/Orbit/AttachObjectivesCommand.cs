using Core;

namespace Commands;

public class AttachObjectivesCommand : Command<AttachObjectivesResponse>
{
    public Guid OrbitId { get; set; }
    public LongTermObjectiveValueItem[] LongTermObjectiveItems { get; set; }
}

public class LongTermObjectiveValueItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
    public ShortTermObjectiveValueItem[] ShortTermObjectiveItems { get; set; }

}

public class ShortTermObjectiveValueItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid YearId { get; set; }
    public Guid OwnerId { get; set; }
    public ActionObjectiveValueItem[] ActionItems { get; set; }
}

public class ActionObjectiveValueItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid WhoId { get; set; }
    public int Order { get; set; }
    public DateTime? OriginalDueDate { get; set; }
}

public class AttachObjectivesResponse
{
}
