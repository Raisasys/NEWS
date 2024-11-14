﻿using Core;
using Core.Domain;
using System.Security.Principal;
using Core.Types;
using Shared.Types;

namespace Domain
{
	[AutoGeneratedKey]
    public class News : Aggregate,IHaveAccess, IHaveCommunications
	{
		protected News() { }

        public News(string title, string summery, NewsContent content,AttachedFile titleImage, DateTime? expirationTime, int expireDuration,  string ownerScopeId)
		{
			Title = title;
			Summery = summery;
			TitleImage = titleImage;
			ExpirationTime = expirationTime;
			ExpireDuration = expireDuration;
			OwnerScopeId = ownerScopeId;
			Content =content;
		}

	

		public string Title { get; set; }
		public string Summery { get; set; }
		public AttachedFile TitleImage { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public string OwnerScopeId { get; set; }
        public bool ShouldAuthenticated { get; set; }

        public ArchiveInfo Archived{ get; set; }
        public PublishInfo Published { get; set; }


        public void Archive(string user)
        {
            Archived = new ArchiveInfo(DateTime.Now, user);
        }

        public void Publish(string user)
        {
            Published = new PublishInfo(DateTime.Now, user);
        }



        public virtual NewsContent Content { get; set; }

        public bool IsGlobal { get; set; }
        public virtual ICollection<AccessEntityValue> AccessEntityItems { get; set; } = new List<AccessEntityValue>();


        public bool HasAccess(IUserIdentity identity) =>
            !IsDeleted && Archived == null && Published != null &&
            (!ShouldAuthenticated || IsGlobal ||(identity != null && this.HaveAccess(identity.Scopes.ToList(), identity.User)));

        public ICollection<CommunicationItem> Communications { get; set; }
    }
}
