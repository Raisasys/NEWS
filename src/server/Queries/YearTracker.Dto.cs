using Core;
using Domain;

namespace Queries;

public class YearTrackerDto : IDto
{
    public Guid OrbitId { get; set; }
    public long OrbitKey { get; set; }
    public string OrbitTitle { get; set; }
    public OwnerCompany OrbitOwner { get; set; }
    public YearDto Year { get; set; }
    public IList<AxisDto> Items { get; set; } = new List<AxisDto>();
    
}


public class AxisDto(
    LongTermObjective longTermObjective, 
    ShortTermObjective shortTermObjective, 
    PersonDto owner, IEnumerable<MonthlyStatusDto> monthlyStatuses) : IDto
{
    public Guid LongTermObjectiveId { get; set; } = longTermObjective.Id;
    public string Milestone { get; set; } = longTermObjective.Title;
    public Guid ShortTermObjectiveId { get; set; } = shortTermObjective.Id;
    public string What { get; set; } = shortTermObjective.Title;
    public PersonDto Owner { get; set; } = owner;
    public IEnumerable<MonthlyStatusDto> MonthlyStatuses { get; set; } = monthlyStatuses;

}

