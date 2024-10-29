﻿using Core;
using Core.Domain;

namespace Domain;

[AutoGeneratedKey]
public class GroupNews : Aggregate, IHaveAccess
{
    protected GroupNews() { }

    public GroupNews(
        string title, string summery, bool isPublished, bool isActive, bool isArchived,
        DateTime? expirationTime, int expireDuration, string ownerScopeId, List<GroupNewsItem> items)
    {
        Title = title;
        Summery = summery;
        IsActive = isActive;
        ExpirationTime = expirationTime;
        ExpireDuration = expireDuration;
        OwnerScopeId = ownerScopeId;
        IsArchived = isArchived;
        Items =items;
    }



    public string Title { get; set; }
    public string Summery { get; set; }
    public bool IsActive { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public virtual ICollection<GroupNewsItem> Items { get; set; } = new List<GroupNewsItem>();

    public bool IsGlobal { get; set; }
    public virtual ICollection<AccessEntityValue> AccessEntityItems { get; set; } = new List<AccessEntityValue>();

    public bool HasAccess(IUserIdentity identity) =>
        IsActive && !IsArchived && 
        (!ShouldAuthenticated || IsGlobal ||(identity != null && this.HaveAccess(identity.Scopes.ToList(), identity.User)));
}