using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead;

public class ConsultantController : AppController
{
    [HttpGet]
    public async Task<ActionResult<ConsultantDto>> GetById([FromQuery]GetConsultantByIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetConsultantByIdQuery, ConsultantDto>(query);
        return Ok(response);
    }


    [HttpGet]
    public async Task<ActionResult<ConsultantListDto>> GetList()
    {
        var response = await QueryProcessor.Execute<GetConsultantListQuery, ConsultantListDto>(new GetConsultantListQuery());
        return Ok(response);
    }
}