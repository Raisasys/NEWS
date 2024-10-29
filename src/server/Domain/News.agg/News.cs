﻿using Core;
using Core.Domain;
using System.Security.Principal;

namespace Domain
{
	[AutoGeneratedKey]
    public class News : Aggregate,IHaveAccess
	{
		protected News() { }

        public News(
			string title, string summery, NewsContent content,
			FileImage titleImage, 
			bool isPublished, bool isActive, bool isArchived, 
			DateTime? expirationTime, int expireDuration,  string ownerScopeId)
		{
			Title = title;
			Summery = summery;
			TitleImage = titleImage;
			IsPublished = isPublished;
			IsActive = isActive;
			ExpirationTime = expirationTime;
			ExpireDuration = expireDuration;
			OwnerScopeId = ownerScopeId;
			IsArchived = isArchived;
			Content =content;
		}

	

		public string Title { get; set; }
		public string Summery { get; set; }
		public FileImage TitleImage { get; set; }
		public bool IsPublished { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public string OwnerScopeId { get; set; }
        public bool ShouldAuthenticated { get; set; }
        public virtual NewsContent Content { get; set; }

        public bool IsGlobal { get; set; }
        public virtual ICollection<AccessEntityValue> AccessEntityItems { get; set; } = new List<AccessEntityValue>();


        public bool HasAccess(IUserIdentity identity) =>
            IsActive && !IsArchived && IsPublished &&
            (!ShouldAuthenticated || IsGlobal ||(identity != null && this.HaveAccess(identity.Scopes.ToList(), identity.User)));

    }
}
