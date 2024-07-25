using Commands;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite;

public class ConsultantController : AppController
{
    [HttpPost]
    public async Task<ActionResult<CreateConsultantResponse>> Create(CreateConsultantCommand command)
    {
        var response = await CommandBus.Send<CreateConsultantCommand, CreateConsultantResponse>(command);
        return Ok(response);
    }

}