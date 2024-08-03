using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Domain
{
    public class News : LongAggregate
	{
		protected News() { }

		private News(
			string title, string summery, NewsContent content,
			string titleImage, 
			bool isPublished, bool isActive, bool isArchived, 
			DateTime? expirationTime, int expireDuration,  Guid ownerScopeId)
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

		public static News Public(
			string title, string summery, NewsContent content,
			string titleImage,
			bool isPublished, bool isActive, bool isArchived,
			DateTime? expirationTime, int expireDuration, Guid ownerScopeId, bool authenticated) => new News(title, summery,
			content, titleImage, isPublished, isActive, isArchived, expirationTime, expireDuration, ownerScopeId)
		{
			Destination = new NewsPublicDestination(authenticated)
		};

		public static News Scope(
			string title, string summery, NewsContent content,
			string titleImage,
			bool isPublished, bool isActive, bool isArchived,
			DateTime? expirationTime, int expireDuration, Guid ownerScopeId, IEnumerable<Guid> scopes) => new News(title, summery,
			content, titleImage, isPublished, isActive, isArchived, expirationTime, expireDuration, ownerScopeId)
		{
			Destination = new NewsScopeDestination(scopes)
		};


		public string Title { get; set; }
		public string Summery { get; set; }
		public string TitleImage { get; set; }
		public bool IsPublished { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public Guid OwnerScopeId { get; set; }
		public virtual NewsContent Content { get; set; }
		public virtual NewsDestination Destination { get; set; }

	}



}
