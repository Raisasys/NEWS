
using Core;
using Domain;
using Newtonsoft.Json;

namespace Commands.GroupNews;

public class CreateGroupNewsCommand : Command<GroupNewsResponse>
{
    public string Title { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ICollection<GroupNewsItem> Items { get; set; }
}


public class PublishGroupNewsCommand : Command
{
    public Guid GroupNewsId { get; set; }
    public bool Published { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }
}


public class ArchiveGroupNewsCommand : Command
{
    public Guid GroupNewsId { get; set; }
    public bool Archived { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
}

public class AuthenticatedGroupNewsCommand : Command
{
    public Guid GroupNewsId { get; set; }
    public bool Authenticated { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
}
public class GroupNewsResponse
{
    public Guid GroupNewsId { get; set; }
}

public class UpdateGroupNewsCommand : Command<GroupNewsResponse>
{
    public Guid GroupNewsId { get; set; }
    public string Title { get; set; }
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