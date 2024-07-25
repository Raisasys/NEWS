using AutoMapper;
using Domain;
using Queries;

namespace QueryServices;

public class CompanyMapperProfile: Profile
{
    public CompanyMapperProfile()
    {
        CreateMap<Company, CompanySimpleDto>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email != null ? s.Email.Value : null))
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber != null ? s.PhoneNumber.Value : null));
            //.ForMember(d => d.Owner, o => o.Ignore());

        CreateMap<Company, CompanyDto>()
            .ForMember(d=>d.Email,o=>o.MapFrom(s=>s.Email != null ? s.Email.Value : null))
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber != null ? s.PhoneNumber.Value : null));
            //.ForMember(d=>d.Owner,o=>o.Ignore());

        CreateMap<Person, PersonDto>();
    }
}