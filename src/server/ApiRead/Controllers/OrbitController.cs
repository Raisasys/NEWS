using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead;

public class OrbitController : AppController
{
    [HttpGet]
    public async Task<ActionResult<OrbitDto>> GetById([FromQuery] GetOrbitByIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetOrbitByIdQuery, OrbitDto>(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<OrbitListDto>> GetListByCompanyId([FromQuery] GetOrbitListByCompanyIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetOrbitListByCompanyIdQuery, OrbitListDto>(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<OrbitListDto>> GetList()
    {
        var response = await QueryProcessor.Execute<GetOrbitListQuery, OrbitListDto>(new GetOrbitListQuery());
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<OrbitYearListDto>> GetYearListByOrbitId([FromQuery] GetOrbitYearListByOrbitIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetOrbitYearListByOrbitIdQuery, OrbitYearListDto>(query);
        return Ok(response);
    }
}