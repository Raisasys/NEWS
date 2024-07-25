using Core;
using Domain;

namespace Queries;

public class OrbitSimpleDto : IDto
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public OwnerCompany Owner { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedById { get; set; }
    public UserValue CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string LastModifiedById { get; set; }
    public UserValue LastModifiedBy { get; set; }
    public IEnumerable<YearDto> Years { get; set; }
    public int StartYear { get; set; }
    public Month StartMonth { get; set; }
    public int YearDuration { get; set; }
}

public class OrbitDto : OrbitSimpleDto
{
    //public MainConsultant MainConsultant { get; set; }
    //public virtual ICollection<ParticipantConsultant> Consultants { get; set; } = new List<ParticipantConsultant>();
    //public IEnumerable<YearDto> Years { get; set; }
    public IEnumerable<LongTermObjectiveDto> LongTermObjectives { get; set; }
}

public class LongTermObjectiveDto : IDto
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
    public IEnumerable<ShortTermObjectiveDto> ShortTermObjectives { get; set; }

}

public class ShortTermObjectiveDto : IDto
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public YearDto Year { get; set; }
    public Guid? OwnerPersonId { get; set; }
    public PersonDto Owner { get; set; }
    public Status Status { get; set; }

    public IEnumerable<MonthlyStatusDto> MonthlyStatuses { get; set; }
    public IEnumerable<ObjectiveActionDto> Actions { get; set; }
}

public class YearDto : IDto
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public int Value { get; set; }
    public YearMonth Start { get; set; }
    public YearMonth End { get; set; }
}

public class MonthlyStatusDto : IDto
{
    public Guid Id { get; set; }
    public Month Month { get; set; }
    public Status Status { get; set; }
}

public class ObjectiveActionDto : IDto
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
    public IEnumerable<ActionCommentDto> Comments { get; set; }

}

public class ActionCommentDto : IDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}

public class OrbitListDto : IListDto<OrbitSimpleDto>
{
    public IEnumerable<OrbitSimpleDto> Items { get; set; }
}


public class OrbitYearListDto : IListDto<YearDto>
{
    public IEnumerable<YearDto> Items { get; set; }
}

