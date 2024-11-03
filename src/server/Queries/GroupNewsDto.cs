
using Core;
using Domain;

namespace Queries;

public class GroupNewsDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summery { get; set; }
    public bool IsActive { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ICollection<GroupNewsItem> Items { get; set; }
}


public class GroupNewsListDto : IListDto<GroupNewsDto>
{
    public IEnumerable<GroupNewsDto> Items { get; set; }
}

