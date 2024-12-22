﻿using Core;
using Core.Domain;
using Core.Types;

namespace Domain;

[AutoGeneratedKey]
public class GroupNews : Aggregate, IHaveAccess, IHaveCommunications
{
    protected GroupNews() { }

    public GroupNews(
        string title, DateTime? expirationTime, int expireDuration, string ownerScopeId, List<GroupNewsItem> items)
    {
        Title = title;
        ExpirationTime = expirationTime;
        ExpireDuration = expireDuration;
        OwnerScopeId = ownerScopeId;
        Items =items;
    }



    public string Title { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public virtual ICollection<GroupNewsItem> Items { get; set; } = new List<GroupNewsItem>();

    public ArchiveInfo Archived { get; set; }
    public PublishInfo Published { get; set; }
    
    public void Archive(string user)
    {
        Archived = new ArchiveInfo(DateTime.Now, user);
    }

    public void Publish(string user)
    {
        Published = new PublishInfo(DateTime.Now, user);
    }

    public bool IsGlobal { get; set; }
    public virtual ICollection<AccessEntityValue> AccessEntityItems { get; set; } = new List<AccessEntityValue>();

    public bool HasAccess(IUserIdentity identity) =>
        !IsDeleted && Archived == null && Published != null &&
        (IsGlobal ||(identity != null && this.HaveAccess(identity.Scopes.ToList(), identity.User)));

    public ICollection<CommunicationItem> Communications { get; set; }
}