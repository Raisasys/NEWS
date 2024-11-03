
using AutoMapper;
using Domain;
using Queries;

namespace QueryServices;

public class GroupNewsMapperProfile : Profile
{
    public GroupNewsMapperProfile()
    {
        CreateMap<GroupNews, GroupNewsDto>();

    }

}
