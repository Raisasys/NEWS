
using AutoMapper;
using Domain;
using Queries;

namespace QueryServices;

public class GroupAnnouncementMapperProfile : Profile
{
    public GroupAnnouncementMapperProfile()
    {
        CreateMap<GroupAnnouncement, GroupAnnouncementDto>();

    }

}
