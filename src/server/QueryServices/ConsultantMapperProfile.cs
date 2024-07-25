using AutoMapper;
using Domain;
using Queries;

namespace QueryServices;

public class ConsultantMapperProfile : Profile
{
    public ConsultantMapperProfile()
    {
        CreateMap<Consultant, ConsultantSimpleDto>()
            .ForMember(d => d.Email, o => o.MapFrom(s=>s.User.Email))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.User.Name))
            .ForMember(d => d.ParticipantsCount, o => o.Ignore());

        CreateMap<Consultant, ConsultantDto>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.User.LastName));
    }
}