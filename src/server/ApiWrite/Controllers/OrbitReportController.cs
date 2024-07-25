using Commands;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite;

public class OrbitReportController: AppController
{
    [HttpPost]
    public async Task<ActionResult> SetExecutionPlan([FromBody] SetExecutionPlanCommand command)
    {
        await CommandBus.Send<SetExecutionPlanCommand>(command);
        return Ok();
    }

    
}