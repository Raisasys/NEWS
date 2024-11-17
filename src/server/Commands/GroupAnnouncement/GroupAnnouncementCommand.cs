
using Core;
using Domain;
using Newtonsoft.Json;

namespace Commands;

public class CreateGroupAnnouncementCommand : Command<GroupAnnouncementResponse>
{
    public string Title { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ICollection<GroupAnnouncementItem> Items { get; set; }
}


public class PublishGroupAnnouncementCommand : Command
{
    public Guid GroupAnnouncementId { get; set; }
    public bool Published { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }
}


public class ArchiveGroupAnnouncementCommand : Command
{
    public Guid GroupAnnouncementId { get; set; }
    public bool Archived { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
}

public class AuthenticatedGroupAnnouncementCommand : Command
{
    public Guid GroupAnnouncementId { get; set; }
    public bool Authenticated { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
}
public class GroupAnnouncementResponse
{
    public Guid GroupAnnouncementId { get; set; }
}

public class UpdateGroupAnnouncementCommand : Command<GroupAnnouncementResponse>
{
    public Guid GroupAnnouncementId { get; set; }
    public string Title { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ICollection<GroupAnnouncementItem> Items { get; set; }
}

public class DeleteGroupAnnouncementCommand : Command
{
    public Guid GroupAnnouncementId { get; set; }
}

public class UpdateIsGlobalGroupAnnouncementCommand : Command
{
    public Guid Id { get; set; }
    public bool IsGlobal { get; set; }
}


public class UpdateHaveAccessGroupAnnouncementCommand : Command
{
    public Guid Id { get; set; }
    public List<string> Scopes { get; set; }
    public List<string> Users { get; set; }
}


public class AttachCommunicationToGroupAnnouncementCommand : Command
{
    public Guid Id { get; set; }
    public CommunicationMessage Message { get; set; }
}