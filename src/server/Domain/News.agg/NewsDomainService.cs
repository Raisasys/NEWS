using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

	public interface INewsDomainService
	{
		News CreateNews (string title, string summery, string titleImage, byte newsType, bool isPublished, bool isActive, bool isArchived, DateTime? expirationTime, int expireDuration, NewsContent content, Guid scopedId);
	}

	public class NewsDomainService : DomainService, INewsDomainService
	{
		public News CreateNews(string title, string summery, string titleImage, byte newsType, bool isPublished,
			bool isActive, bool isArchived, DateTime? expirationTime, int expireDuration, NewsContent content,
			Guid scopedId)
		{
			var newNews = new News(title, summery, titleImage, newsType, isPublished, isActive, isArchived,
				expirationTime, expireDuration, content, scopedId);

			Database.Add(newNews);
			return newNews;
		}
	}

}
