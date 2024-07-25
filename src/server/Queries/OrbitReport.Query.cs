using Core;

namespace Queries;

public class GetExecutionPlanQuery : IQuery<ExecutionPlanDto>
{
    public Guid OrbitId { get; set; }
    public Guid YearId { get; set; }

}

public class GetYearTrackerQuery : IQuery<YearTrackerDto>
{
    public Guid OrbitId { get; set; }
    public Guid YearId { get; set; }

}
