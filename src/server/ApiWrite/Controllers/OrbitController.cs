using Commands;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite;

public class OrbitController: AppController
{
    [HttpPost]
    public async Task<ActionResult<CreateOrbitResponse>> Create([FromBody] CreateOrbitCommand command)
    {
        var response = await CommandBus.Send<CreateOrbitCommand, CreateOrbitResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddLongTermObjectiveResponse>> AddLongTermObjective([FromBody] AddLongTermObjectiveCommand command)
    {
        var response = await CommandBus.Send<AddLongTermObjectiveCommand, AddLongTermObjectiveResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddShortTermObjectiveResponse>> AddShortTermObjective([FromBody] AddShortTermObjectiveCommand command)
    {
        var response = await CommandBus.Send<AddShortTermObjectiveCommand, AddShortTermObjectiveResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddActionResponse>> AddAction([FromBody] AddActionCommand command)
    {
        var response = await CommandBus.Send<AddActionCommand, AddActionResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddConsultantResponse>> AddConsultant([FromBody] AddConsultantCommand command)
    {
        var response = await CommandBus.Send<AddConsultantCommand, AddConsultantResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SetShortTermObjectiveStatusResponse>> SetShortTermObjectiveStatus([FromBody] SetShortTermObjectiveStatusCommand command)
    {
        var response = await CommandBus.Send<SetShortTermObjectiveStatusCommand, SetShortTermObjectiveStatusResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SetActionStatusResponse>> SetActionStatus([FromBody] SetActionStatusCommand command)
    {
        var response = await CommandBus.Send<SetActionStatusCommand, SetActionStatusResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddActionCommentResponse>> AddActionComment([FromBody] AddActionCommentCommand command)
    {
        var response = await CommandBus.Send<AddActionCommentCommand, AddActionCommentResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AttachObjectivesResponse>> AttachObjectives([FromBody] AttachObjectivesCommand command)
    {
        var response = await CommandBus.Send<AttachObjectivesCommand, AttachObjectivesResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AttachObjectivesResponse>> AttachLongTermObjective([FromBody] AttachLongTermObjectiveCommand command)
    {
        var response = await CommandBus.Send<AttachLongTermObjectiveCommand, AttachObjectivesResponse>(command);
        return Ok(response);
    }
    
}