using Core;
using Domain;

namespace Commands;

public class SetExecutionPlanCommand : Command
{
    public Guid OrbitId { get; set; }
    public MilestoneItem[] MilestoneItems { get; set; }
    
}

public class MilestoneItem
{
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public MilestoneActionItem[] ActionItems { get; set; }

}

public class MilestoneActionItem
{
    public Guid Id { get; set; }
    //public Guid WhoPersonId { get; set; }
    public Status CurrentStatus { get; set; }
    public Status PreviousStatus { get; set; }
    public DateTime OriginalDueDate { get; set; }
    public DateTime? NewDueDate { get; set; }
    public ImportantUrgent ImportantUrgent { get; set; }
    public int StartDuration { get; set; }
    public int EndDuration { get; set; }
}