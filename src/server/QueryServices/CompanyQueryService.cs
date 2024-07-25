using AutoMapper;
using Core;
using Domain;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices;

public class CompanyQueryService : QueryService,
    IQueryService<GetCompanyListQuery, CompanyListDto>,
    IQueryService<GetCompanyByIdQuery, CompanyDto>,
    IQueryService<GetOwnerByCompanyIdOwnerIdQuery, PersonDto>,
    IQueryService<GetOwnersByCompanyIdQuery, CompanyOwnerListDto>,
    IQueryService<GetWhoByCompanyIdWhoIdQuery, PersonDto>,
    IQueryService<GetWhosByCompanyIdQuery, CompanyWhoListDto>
{
    private readonly IMapper _mapper;

    public CompanyQueryService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<CompanyListDto> Execute(GetCompanyListQuery query, CancellationToken cancellationToken)
    {
        var items = await Database.Set<Company>().Include(c=>c.Personnel).ToListAsync(cancellationToken: cancellationToken);
        var dtos = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanySimpleDto>>(items);
        /*var dtos = new List<CompanySimpleDto>(); 
        foreach (var item in items)
        {
            var dto = _mapper.Map<Company, CompanySimpleDto>(item);
            var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
            if (owner != null)
                dto.Owner = _mapper.Map<Person, PersonDto>(owner);
            dtos.Add(dto);
        }*/
        return new CompanyListDto
        {
            Items = dtos
        };
    }

    public async Task<CompanyDto> Execute(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Find<Company>(query.CompanyId);
        var dto = _mapper.Map<Company, CompanyDto>(item);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return dto;
    }

    public async Task<CompanyOwnerListDto> Execute(GetOwnersByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Set<Company>().Include(c=>c.Personnel).FirstOrDefaultAsync(c=>c.Id == query.CompanyId,cancellationToken);
        var owners = item.Personnel.Where(i => i.Role == PersonRole.Owner || i.Role == PersonRole.Both).ToList();
        var dtos = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(owners);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return new CompanyOwnerListDto
        {
            Items = dtos
        };
    }

    public async Task<PersonDto> Execute(GetOwnerByCompanyIdOwnerIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Set<Company>().Include(c => c.Personnel).FirstOrDefaultAsync(c => c.Id == query.CompanyId, cancellationToken);
        var owners = item.Personnel.Where(i => i.Role == PersonRole.Owner || i.Role == PersonRole.Both).ToList();
        var owner = owners.FirstOrDefault(c => c.Id == query.OwnerId);
        var dto = _mapper.Map<Person, PersonDto>(owner);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return dto;
    }

    public async Task<CompanyWhoListDto> Execute(GetWhosByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Set<Company>().Include(c => c.Personnel).FirstOrDefaultAsync(c => c.Id == query.CompanyId, cancellationToken);
        var owners = item.Personnel.Where(i => i.Role == PersonRole.Who || i.Role == PersonRole.Both).ToList();
        var dtos = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(owners);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return new CompanyWhoListDto
        {
            Items = dtos
        };
    }

    public async Task<PersonDto> Execute(GetWhoByCompanyIdWhoIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Set<Company>().Include(c => c.Personnel).FirstOrDefaultAsync(c => c.Id == query.CompanyId, cancellationToken);
        var owners = item.Personnel.Where(i => i.Role == PersonRole.Who || i.Role == PersonRole.Both).ToList();
        var owner = owners.FirstOrDefault(c => c.Id == query.WhoId);
        var dto = _mapper.Map<Person, PersonDto>(owner);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return dto;
    }
}