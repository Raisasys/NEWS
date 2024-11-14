
using Core;
using Domain;

namespace Queries;

public class GroupNewsDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ArchiveInfo Archived { get; set; }
    public PublishInfo Published { get; set; }
    public IList<GroupNewsItem> Items { get; set; }
}


public class GroupNewsListDto : IListDto<GroupNewsDto>
{
    public IEnumerable<GroupNewsDto> Items { get; set; }
}

