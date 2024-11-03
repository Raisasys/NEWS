
using Core;
using Domain;

namespace Commands.GroupNews;

public class GroupNewsCommand : Command<GroupNewsResponse>
{
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

public class GroupNewsResponse
{
    public Guid GroupNewsId { get; set; }
}

public class GroupNewsUpdateCommand : Command<GroupNewsResponse>
{
    public Guid GroupNewsId { get; set; }
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

public class DeleteGroupNewsCommand : Command
{
    public Guid GroupNewsId{ get; set; }
}

public class UpdateIsGlobalGroupNewsCommand : Command
{
    public Guid Id { get; set; }
    public bool IsGlobal { get; set; }
}


public class UpdateHaveAccessGroupNewsCommand : Command
{
    public Guid Id { get; set; }
    public List<string> Scopes { get; set; }
    public List<string> Users { get; set; }
}


public class AttachCommunicationToGroupNewsCommand : Command
{
    public Guid Id { get; set; }
    public CommunicationMessage Message { get; set; }
}