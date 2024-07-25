using AutoMapper;
using Core;
using Domain;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices;

public class ConsultantQueryService : QueryService,
    IQueryService<GetConsultantListQuery, ConsultantListDto>,
    IQueryService<GetConsultantByIdQuery, ConsultantDto>
{
    private readonly IMapper _mapper;

    public ConsultantQueryService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ConsultantListDto> Execute(GetConsultantListQuery query, CancellationToken cancellationToken)
    {
        var items = await Database.Set<Consultant>().ToListAsync(cancellationToken: cancellationToken);
        var orbits = await Database.Set<Orbit>().Include(c=>c.Consultants).ToListAsync(cancellationToken);
        var participants = orbits.SelectMany(s => s.Consultants).Select(s => s.ConsultantId).ToList();
        var dtos = _mapper.Map<IEnumerable<Consultant>, IEnumerable<ConsultantSimpleDto>>(items);
        foreach(var dto in dtos)
            dto.ParticipantsCount = participants.Count(d => d == dto.Id);
        
        return new ConsultantListDto
        {
            Items = dtos
        };
    }

    public async Task<ConsultantDto> Execute(GetConsultantByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Find<Consultant>(query.ConsultantId);
        var dto = _mapper.Map<Consultant, ConsultantDto>(item);
        /*var owner = item.Personnel.FirstOrDefault(i => i.Id == item.Owner.PersonId);
        if (owner != null)
            dto.Owner = _mapper.Map<Person, PersonDto>(owner);*/
        return dto;
    }
}