using Commands;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite;

public class CompanyController : AppController
{
    [HttpPost]
    public async Task<ActionResult<CreateCompanyResponse>> Create(CreateCompanyCommand command)
    {
        var response = await CommandBus.Send<CreateCompanyCommand, CreateCompanyResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddPersonnelToCompanyResponse>> AddOwner(AddOwnerToCompanyCommand command)
    {
        var response = await CommandBus.Send<AddOwnerToCompanyCommand, AddPersonnelToCompanyResponse>(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AddPersonnelToCompanyResponse>> AddWho(AddWhoToCompanyCommand command)
    {
        var response = await CommandBus.Send<AddWhoToCompanyCommand, AddPersonnelToCompanyResponse>(command);
        return Ok(response);
    }
}