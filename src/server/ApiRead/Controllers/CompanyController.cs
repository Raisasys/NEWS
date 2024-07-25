using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead;

public class CompanyController : AppController
{
    [HttpGet]
    public async Task<ActionResult<CompanyDto>> GetById([FromQuery] GetCompanyByIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetCompanyByIdQuery, CompanyDto>(query);
        return Ok(response);
    }


    [HttpGet]
    public async Task<ActionResult<CompanyListDto>> GetList()
    {
        var response = await QueryProcessor.Execute<GetCompanyListQuery, CompanyListDto>(new GetCompanyListQuery());
        return Ok(response);
    }


    [HttpGet]
    public async Task<ActionResult<CompanyOwnerListDto>> GetOwnerList([FromQuery] GetOwnersByCompanyIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetOwnersByCompanyIdQuery, CompanyOwnerListDto>(query);
        return Ok(response);
    }



    [HttpGet]
    public async Task<ActionResult<PersonDto>> GetOwnerByCompanyIdOwnerId([FromQuery] GetOwnerByCompanyIdOwnerIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetOwnerByCompanyIdOwnerIdQuery, PersonDto>(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<CompanyWhoListDto>> GetWhoList([FromQuery] GetWhosByCompanyIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetWhosByCompanyIdQuery, CompanyWhoListDto>(query);
        return Ok(response);
    }



    [HttpGet]
    public async Task<ActionResult<PersonDto>> GetWhoByCompanyIdWhoId([FromQuery] GetWhoByCompanyIdWhoIdQuery query)
    {
        var response = await QueryProcessor.Execute<GetWhoByCompanyIdWhoIdQuery, PersonDto>(query);
        return Ok(response);
    }
}