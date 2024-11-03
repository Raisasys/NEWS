
using Core;

namespace Queries;

    public class GroupNewsByIdQuery : IQuery<GroupNewsDto>
    {
        public Guid GroupNewsId { get; set; }
    }

    public class GetGroupNewsListQuery : IQuery<GroupNewsListDto>
    {
    }

    public class GetGroupNewsHaveAccessQuery : IQuery<HaveAccessDto>
    {
        public Guid Id { get; set; }
    }



    public class GetGroupNewsHaveCommunicationsQuery : IQuery<CommunicationItemListDto>
    {
        public Guid Id { get; set; }
    }

