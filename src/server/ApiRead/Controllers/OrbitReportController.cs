using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead;

public class OrbitReportController : AppController
{
    [HttpGet]
    public async Task<ActionResult<ExecutionPlanDto>> GetExecutionPlan([FromQuery] GetExecutionPlanQuery query)
    {
        var response = await QueryProcessor.Execute<GetExecutionPlanQuery, ExecutionPlanDto>(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<YearTrackerDto>> GetYearTracker([FromQuery] GetYearTrackerQuery query)
    {
        var response = await QueryProcessor.Execute<GetYearTrackerQuery, YearTrackerDto>(query);
        return Ok(response);
    }
}