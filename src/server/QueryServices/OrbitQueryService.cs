using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices;

public class OrbitQueryService : QueryService,
    IQueryService<GetOrbitByIdQuery, OrbitDto>,
    IQueryService<GetOrbitListQuery, OrbitListDto>,
    IQueryService<GetOrbitListByCompanyIdQuery, OrbitListDto>,
    IQueryService<GetOrbitYearListByOrbitIdQuery, OrbitYearListDto>
{
    private readonly IMapper _mapper;

    public OrbitQueryService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<OrbitDto> Execute(GetOrbitByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var dataQuery = Database.Set<Orbit>()
                .Include(c => c.LongTermObjectives)
                .ThenInclude(c => c.ShortTermObjectives)
                .ThenInclude(c => c.Actions)
                .ThenInclude(c => c.Statuses)
                .Include(c => c.Years);
            var item = query.OrbitId == Guid.Empty ? 
                await dataQuery.FirstOrDefaultAsync(cancellationToken) :
                await dataQuery.SingleOrDefaultAsync(i => i.Id == query.OrbitId, cancellationToken: cancellationToken);
            

            if (item == null) return null;

            var company = await Database.Set<Company>().Include(c => c.Personnel)
                .SingleAsync(i => i.Id == item.Owner.CompanyId, cancellationToken: cancellationToken);
            var companyPersons = company.Personnel.ToList();

            var dto = _mapper.Map<Orbit, OrbitDto>(item);
            var orderedYears = dto.Years.OrderBy(i => i.Value);

            foreach (var longTermObjective in dto.LongTermObjectives)
            {
                foreach (var shortTermObjective in longTermObjective.ShortTermObjectives)
                {
                    if (shortTermObjective.OwnerPersonId.HasValue)
                    {
                        var ownerPerson =
                            companyPersons.FirstOrDefault(i => i.Id == shortTermObjective.OwnerPersonId.Value);
                        if (ownerPerson != null)
                            shortTermObjective.Owner = _mapper.Map<Person, PersonDto>(ownerPerson);
                    }

                    foreach (var objectiveAction in shortTermObjective.Actions)
                    {
                        if (objectiveAction.WhoPersonId.HasValue)
                        {
                            var whoPerson =
                                companyPersons.FirstOrDefault(i => i.Id == objectiveAction.WhoPersonId.Value);
                            if (whoPerson != null)
                                objectiveAction.Who = _mapper.Map<Person, PersonDto>(whoPerson);
                        }
                    }
                }
            }

            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OrbitListDto> Execute(GetOrbitListQuery query, CancellationToken cancellationToken)
    {
        var users = await Database.Set<User>().ToListAsync(cancellationToken);
        var items = await Database.Set<Orbit>().Include(c => c.Years).ToListAsync(cancellationToken);
        var dtos = _mapper.Map<IEnumerable<Orbit>, IEnumerable<OrbitSimpleDto>>(items);
        foreach (var dto in dtos)
        {
            var createdByUser = users.FirstOrDefault(i => i.Id == dto.CreatedById);
            dto.CreatedBy = createdByUser != null ? createdByUser.ToValue() : null;

            var lastModifiedByUser = users.FirstOrDefault(i => i.Id == dto.LastModifiedById);
            dto.LastModifiedBy = lastModifiedByUser != null ? lastModifiedByUser.ToValue() : null;
        }
        return new OrbitListDto
        {
            Items = dtos
        };
    }

    public async Task<OrbitListDto> Execute(GetOrbitListByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        var users = await Database.Set<User>().ToListAsync(cancellationToken);
        var items = await Database.Set<Orbit>().Include(c=>c.Years).Where(i=>i.Owner.CompanyId == query.CompanyId).ToListAsync(cancellationToken);
        var dtos = _mapper.Map<IEnumerable<Orbit>, IEnumerable<OrbitSimpleDto>>(items);
        foreach (var dto in dtos)
        {
            var createdByUser = users.FirstOrDefault(i => i.Id == dto.CreatedById);
            dto.CreatedBy = createdByUser != null ? createdByUser.ToValue() : null;

            var lastModifiedByUser = users.FirstOrDefault(i => i.Id == dto.LastModifiedById);
            dto.LastModifiedBy = lastModifiedByUser != null ? lastModifiedByUser.ToValue() : null;
        }
        return new OrbitListDto
        {
            Items = dtos
        };
    }

    public async Task<OrbitYearListDto> Execute(GetOrbitYearListByOrbitIdQuery query, CancellationToken cancellationToken)
    {
        var item = await Database.Set<Orbit>().Include(c => c.Years).SingleOrDefaultAsync(i => i.Id== query.OrbitId,cancellationToken);
        var years = _mapper.Map<IEnumerable<Year>, IEnumerable<YearDto>>(item.Years);
        return new OrbitYearListDto
        {
            Items = years
        };
    }
}