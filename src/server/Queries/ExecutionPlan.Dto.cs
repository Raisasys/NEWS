using Core;
using Domain;

namespace Queries;

public class ExecutionPlanDto : IDto
{
    public Guid OrbitId { get; set; }
    public long OrbitKey { get; set; }
    public string OrbitTitle { get; set; }
    public OwnerCompany OrbitOwner { get; set; }
    public YearDto Year { get; set; }
    public IList<MilestoneDto> MilestoneItems { get; set; } = new List<MilestoneDto>();
    public IList<StepDuration> StepDurationItems { get; set; } = new List<StepDuration>();
    
}


public class MilestoneDto(LongTermObjective longTermObjective, ShortTermObjective shortTermObjective, PersonDto owner, IEnumerable<MilestoneActionDto> actionItems) : IDto
{
    public Guid LongTermObjectiveId { get; set; } = longTermObjective.Id;
    public string Milestone { get; set; } = longTermObjective.Title;
    public Guid ShortTermObjectiveId { get; set; } = shortTermObjective.Id;
    public string What { get; set; } = shortTermObjective.Title;
    public PersonDto Owner { get; set; } = owner;
    public IEnumerable<MilestoneActionDto> ActionItems { get; set; } = actionItems;

}


public class MilestoneActionDto : IDto
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
    public Guid? WhoPersonId { get; set; }
    public PersonDto Who { get; set; }

    public Status PreviousStatus { get; set; }
    public Status CurrentStatus { get; set; }

    public DateTime? OriginalDueDate { get; set; }
    public DateTime? NewDueDate { get; set; }
    public ImportantUrgent ImportantUrgent { get; set; }
    public ActionDuration Duration { get; set; }

}


public class StepDuration
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public int Week { get; set; }
    public string Legend { get; set; }
    public int Value => int.Parse($"{Year}{ToMonthString()}{Week}");

    public string ToMonthString()=> Month < Month.October ? $"0{(int)Month}" : $"{(int)Month}";
}

