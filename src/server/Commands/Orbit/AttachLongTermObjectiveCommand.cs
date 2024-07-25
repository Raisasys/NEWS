using Core;

namespace Commands;

public class AttachLongTermObjectiveCommand : Command<AttachObjectivesResponse>
{
    public Guid OrbitId { get; set; }
    public LongTermObjectiveValueItem LongTermObjectiveItem { get; set; }
}