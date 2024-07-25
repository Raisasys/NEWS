using AutoMapper;
using Domain;
using Queries;

namespace QueryServices;

public class OrbitMapperProfile: Profile
{
    public OrbitMapperProfile()
    {
        CreateMap<Orbit, OrbitSimpleDto>()
            .ForMember(s => s.CreatedById, o => o.MapFrom(s => s.CreatedBy))
            .ForMember(s => s.CreatedBy, o => o.Ignore())
            .ForMember(s => s.LastModifiedBy, o => o.Ignore())
            .ForMember(s => s.LastModifiedById, o => o.MapFrom(s => s.LastModifiedBy))
            .ForMember(s => s.StartYear, o => o.MapFrom(s=>s.StartYear))
            .ForMember(s => s.StartMonth, o => o.MapFrom(s => s.StartMonth))
            .ForMember(s => s.YearDuration, o => o.MapFrom(s => s.YearDuration));
        
        CreateMap<Orbit, OrbitDto>()
            .ForMember(s => s.CreatedById, o => o.MapFrom(s => s.CreatedBy))
            .ForMember(s => s.CreatedBy, o => o.Ignore())
            .ForMember(s => s.LastModifiedBy, o => o.Ignore())
            .ForMember(s => s.LastModifiedById, o => o.MapFrom(s => s.LastModifiedBy))
            .ForMember(s => s.StartYear, o => o.MapFrom(s => s.StartYear))
            .ForMember(s => s.StartMonth, o => o.MapFrom(s => s.StartMonth))
            .ForMember(s => s.YearDuration, o => o.MapFrom(s => s.YearDuration));

        CreateMap<LongTermObjective, LongTermObjectiveDto>();
        CreateMap<ShortTermObjective, ShortTermObjectiveDto>()
            .ForMember(s => s.OwnerPersonId, o => o.MapFrom(s=>s.Owner !=null ? s.Owner.PersonId : (Guid?)null))
            .ForMember(s=>s.Owner,o=>o.Ignore());

        CreateMap<Year, YearDto>();

        CreateMap<MonthlyStatus, MonthlyStatusDto>();

        CreateMap<ObjectiveAction, ObjectiveActionDto>()
            .ForMember(s => s.WhoPersonId, o => o.MapFrom(s => s.Who != null ? s.Who.PersonId : (Guid?)null))
            .ForMember(s => s.Who, o => o.Ignore());

        CreateMap<ActionComment, ActionCommentDto>();
    }
}