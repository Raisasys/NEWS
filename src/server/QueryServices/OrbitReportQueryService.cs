using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices;

public class OrbitReportQueryService : QueryService,
    IQueryService<GetExecutionPlanQuery, ExecutionPlanDto>,
    IQueryService<GetYearTrackerQuery, YearTrackerDto>
{
    private readonly IMapper _mapper;

    public OrbitReportQueryService(IMapper mapper)
    {
        _mapper = mapper;
    }
    


    public async Task<ExecutionPlanDto> Execute(GetExecutionPlanQuery query, CancellationToken cancellationToken)
    {
        var orbit = await Database.Set<Orbit>()
            .Include(c => c.LongTermObjectives)
            .ThenInclude(c => c.ShortTermObjectives)
            .ThenInclude(c => c.Actions)
            .ThenInclude(c => c.Statuses)
            .Include(c => c.Years)
            .SingleOrDefaultAsync(i => i.Id == query.OrbitId, cancellationToken: cancellationToken);

        if (orbit == null ) return null;

        var year = orbit.Years.FirstOrDefault(y => y.Id == query.YearId);

        if (year == null) return null;

        var company = await Database.Set<Company>().Include(c => c.Personnel)
            .SingleAsync(i => i.Id == orbit.Owner.CompanyId, cancellationToken: cancellationToken);
        var companyPersons = company.Personnel.ToList();
        var companyPersonDtos = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(companyPersons);

        var executionPlanDto = _mapper.Map<Orbit, ExecutionPlanDto>(orbit);
        var yearDto = _mapper.Map<Year, YearDto>(year);
        executionPlanDto.Year = yearDto;

        for (Month i = year.Start.Month; i <= Month.December; i++)
        {
            for (int w = 1; w <= 4; w++)
            {
                string legend = null;
                if (w==1)
                {
                    legend = i.ToName();
                }
                executionPlanDto.StepDurationItems.Add(new StepDuration
                {
                    Year= year.Start.Year,
                    Legend = legend,
                    Month = i,
                    Week = w
                });
            }
        }

        if (year.End.Year > year.Start.Year)
        {
            for (Month i = Month.January; i <= year.End.Month; i++)
            {
                for (int w = 1; w <= 4; w++)
                {
                    executionPlanDto.StepDurationItems.Add(new StepDuration
                    {
                        Year = year.End.Year,
                        Legend = i.ToName(),
                        Month = i,
                        Week = w
                    });
                }
            }
        }

        

        foreach (var longTerm in orbit.LongTermObjectives)
        {
            var shortTermObjectives = longTerm.ShortTermObjectives.Where(s => s.Year.Id == year.Id).ToList();

            foreach (var shortTerm in shortTermObjectives)
            {
                var owner = companyPersonDtos.FirstOrDefault(p => p.Id == shortTerm.Owner.PersonId);
                var actionDtos = _mapper.Map<IEnumerable<ObjectiveAction>, IEnumerable<MilestoneActionDto>>(shortTerm.Actions);

                foreach (var action in actionDtos)
                {
                    if (action.WhoPersonId.HasValue)
                    {
                        var whoPerson = companyPersons.FirstOrDefault(i => i.Id == action.WhoPersonId.Value);
                        if (whoPerson != null)
                            action.Who = _mapper.Map<Person, PersonDto>(whoPerson);
                    }
                }

                var milestone = new MilestoneDto(longTerm, shortTerm, owner, actionDtos);
                executionPlanDto.MilestoneItems.Add(milestone);
            }
        }

        return executionPlanDto;
    }

    public async Task<YearTrackerDto> Execute(GetYearTrackerQuery query, CancellationToken cancellationToken)
    {
        var orbit = await Database.Set<Orbit>()
            .Include(c => c.LongTermObjectives)
            .ThenInclude(c => c.ShortTermObjectives)
            .ThenInclude(c => c.Actions)
            .ThenInclude(c => c.Statuses)
            .Include(c => c.Years)
            .SingleOrDefaultAsync(i => i.Id == query.OrbitId, cancellationToken: cancellationToken);

        if (orbit == null) return null;

        var year = orbit.Years.FirstOrDefault(y => y.Id == query.YearId);

        if (year == null) return null;

        var company = await Database.Set<Company>().Include(c => c.Personnel)
            .SingleAsync(i => i.Id == orbit.Owner.CompanyId, cancellationToken: cancellationToken);
        var companyPersons = company.Personnel.ToList();
        var companyPersonDtos = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(companyPersons);

        var yearTrackerDto = _mapper.Map<Orbit, YearTrackerDto>(orbit);
        var yearDto = _mapper.Map<Year, YearDto>(year);
        yearTrackerDto.Year = yearDto;

        var axisDtoItems = new List<AxisDto>();
        foreach (var longTerm in orbit.LongTermObjectives)
        {
            var shortTermObjectives = longTerm.ShortTermObjectives.Where(s => s.Year.Id == year.Id).ToList();

            foreach (var shortTerm in shortTermObjectives)
            {
                var owner = companyPersonDtos.FirstOrDefault(p => p.Id == shortTerm.Owner.PersonId);
                var monthlyStatuses = _mapper.Map<IEnumerable<MonthlyStatus>, IEnumerable<MonthlyStatusDto>>(shortTerm.MonthlyStatuses);
                var dto = new AxisDto(longTerm, shortTerm, owner, monthlyStatuses);
                axisDtoItems.Add(dto);
            }
        }

        yearTrackerDto.Items = axisDtoItems;
        return yearTrackerDto;
    }
}