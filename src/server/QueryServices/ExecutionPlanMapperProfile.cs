using AutoMapper;
using Core;
using Domain;
using Queries;

namespace QueryServices;

public class ExecutionPlanMapperProfile : Profile
{
    public ExecutionPlanMapperProfile()
    {
        CreateMap<Orbit, ExecutionPlanDto>()
            .ForMember(d => d.OrbitId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.OrbitKey, o => o.MapFrom(s => s.Key))
            .ForMember(d => d.OrbitTitle, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.OrbitOwner, o => o.MapFrom(s => s.Owner))
            .ForMember(d => d.Year, o => o.Ignore())
            .ForMember(d => d.MilestoneItems, o => o.Ignore());
        
        CreateMap<ObjectiveAction, MilestoneActionDto>()
            .ForMember(s => s.WhoPersonId, o => o.MapFrom(s => s.Who != null ? s.Who.PersonId : (Guid?)null))
            .ForMember(s => s.ImportantUrgent, o => o.MapFrom(s => s.ImportantUrgent ?? new ImportantUrgent(0,0)))
            .ForMember(s => s.PreviousStatus, o => o.MapFrom(s => s.PreviousStatus ?? new Status(null, null)))
            .ForMember(s => s.CurrentStatus, o => o.MapFrom(s => s.CurrentStatus ?? new Status(null, null)))
            .ForMember(s => s.Duration, o => o.MapFrom(s => s.Duration ?? new ActionDuration(null, null, null, null,null,null)))
            .ForMember(s => s.Who, o => o.Ignore());


        CreateMap<Orbit, YearTrackerDto>()
            .ForMember(d => d.OrbitId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.OrbitKey, o => o.MapFrom(s => s.Key))
            .ForMember(d => d.OrbitTitle, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.OrbitOwner, o => o.MapFrom(s => s.Owner))
            .ForMember(d => d.Year, o => o.Ignore())
            .ForMember(d => d.Items, o => o.Ignore());

    }
}