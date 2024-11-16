using Core;

namespace Queries;

public class GetAnnouncementById : IQuery<AnnouncementFullDto>
{
    public Guid AnnouncementId { get; set; }
}

public class GetAnnouncementListDto : IQuery<AnnouncementListDto>
{
}

public class GetAnnounceHaveAccessQuery : IQuery<HaveAccessDto>
{
    public Guid Id { get; set; }
}



public class GetAnnounceHaveCommunicationsQuery : IQuery<CommunicationItemListDto>
{
    public Guid Id { get; set; }
}

public class GetMyAnnounceById : IQuery<AnnouncementFullDto>
{
    public Guid AnnouncementId { get; set; }
}


