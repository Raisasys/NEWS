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
		public News()
		{
				
		}

		public News(string title, string summery, string titleImage, byte newsType, bool isPublished, bool isActive, bool isArchived, DateTime? expirationTime, int expireDuration, NewsContent content, Guid scopedId)
		{
			Title = title;
			Summery = summery;
			TitleImage = titleImage;
			NewsType = newsType;
			IsPublished = isPublished;
			IsActive = isActive;
			ExpirationTime = expirationTime;
			Content = content;
			ExpireDuration = expireDuration;
			ScopeId = scopedId;
			IsArchived = isArchived;
		}
		public long NewsId { get; set; }
		public string Title { get; set; }
		public string Summery { get; set; }
		public string TitleImage { get; set; }
		public byte NewsType { get; set; }
		public bool IsPublished { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public virtual NewsContent Content { get; set; }
		public Guid ScopeId { get; set; }
		public long ContentId { get; set; }


	}



}
