
using Core;
using Domain;

namespace Queries;

public class GroupAnnouncementDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string OwnerScopeId { get; set; }
    public ArchiveInfo Archived { get; set; }
    public PublishInfo Published { get; set; }
    public IList<GroupAnnouncementItem> Items { get; set; }
}


public class GroupAnnouncementListDto : IListDto<GroupAnnouncementDto>
{
    public IEnumerable<GroupAnnouncementDto> Items { get; set; }
}

