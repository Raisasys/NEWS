
using Core;

namespace Queries;

    public class GroupAnnouncementByIdQuery : IQuery<GroupAnnouncementDto>
    {
        public Guid GroupAnnouncementId { get; set; }
    }

    public class GroupAnnouncementListQuery : IQuery<GroupAnnouncementListDto>
    {
    }

    public class GetGroupAnnouncementHaveAccessQuery : IQuery<HaveAccessDto>
    {
        public Guid Id { get; set; }
    }



    public class GetGroupAnnouncementHaveCommunicationsQuery : IQuery<CommunicationItemListDto>
    {
        public Guid Id { get; set; }
    }

