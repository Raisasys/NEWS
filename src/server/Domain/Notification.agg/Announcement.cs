﻿using Core;
using Core.Domain;
using Core.Types;
using Shared.Types;

namespace Domain
{
	[AutoGeneratedKey]
	public class Announcement : Aggregate, IHaveAccess, IHaveCommunications
	{
		public Announcement()
		{
				
		}

		public Announcement(string title, string header, AttachedFile titleImage, string description, ICollection<AnnouncementFile> files)
		{
			Title = title;
            Header = header;
            TitleImage = titleImage;
			Description = description;
			Files = files;
		}

		public string Title { get; set; }
        public string Header { get; set; }
		public string Description { get; set; }
        public AttachedFile TitleImage { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int ExpireDuration { get; set; }
        public string OwnerScopeId { get; set; }
        public bool ShouldAuthenticated { get; set; }

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


        public virtual ICollection<AnnouncementFile> Files { get; set; }
        public bool IsGlobal { get; set; }
        public virtual ICollection<AccessEntityValue> AccessEntityItems { get; set; }
        public virtual ICollection<CommunicationItem> Communications { get; set; }

        public bool HasAccess(IUserIdentity identity) =>
            !IsDeleted && Archived == null && Published != null &&
            (!ShouldAuthenticated || IsGlobal || (identity != null && this.HaveAccess(identity.Scopes.ToList(), identity.User)));
    }
}
