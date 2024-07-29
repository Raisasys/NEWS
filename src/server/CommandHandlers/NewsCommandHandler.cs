using Commands;
using Commands.News;
using Core;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace CommandHandlers
{
	public class NewsCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateNewsByTopImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<UpdateNewsByTopImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByBottomImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByTopBottomImageContentCommand, UpdateNewsResponse>

	{
		private readonly INewsDomainService _newsDomainService;
		public NewsCommandHandler(INewsDomainService newsDomainService)
		{
			_newsDomainService = newsDomainService;
		}

		public async Task<CreateNewsResponse> Handle(CreateNewsByTopImageContentCommand command, CancellationToken cancellationToken)
		{
			var info = command.Info;

			var content = new TopImageContent
			{
				Image = command.Image,
				Text = command.Text
			};

			var newNews = new News(info.Title, info.Summery, content ,info.TitleImage, info.NewsType, info.IsPublished, info.IsActive, 
				info.IsArchived, info.ExpirationTime, info.ExpireDuration, info.ScopeId);

			Database.Add(newNews);
			await Database.SaveChanges(cancellationToken);

			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};

		}

		public async Task<CreateNewsResponse> Handle(CreateNewsByTopBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var info = command.Info;

			var content = new TopBottomImageContent
			{
				TopImage = command.TopImage,
				BottomImage = command.BottomImage,
				Text = command.Text
			};

			var newNews = new News(info.Title, info.Summery, content, info.TitleImage, info.NewsType, info.IsPublished, info.IsActive,
				info.IsArchived, info.ExpirationTime, info.ExpireDuration, info.ScopeId);

			Database.Add(newNews);
			await Database.SaveChanges(cancellationToken);

			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};
		}


		public async Task<CreateNewsResponse> Handle(CreateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var info = command.Info;

			var content = new BottomImageContent
			{
				Image = command.Image,
				Text = command.Text
			};

			var newNews = new News(info.Title, info.Summery, content, info.TitleImage, info.NewsType, info.IsPublished, info.IsActive,
				info.IsArchived, info.ExpirationTime, info.ExpireDuration, info.ScopeId);

			Database.Add(newNews);
			await Database.SaveChanges(cancellationToken);

			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};
		}

		public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = Database.Set<News>().Include(t=>t.Content).SingleOrDefaultAsync(t=>t.Id == command.Info.NewsID).Result;



			var content = updateNews.Content as TopImageContent;
			content.Image = command.Image;
			content.Text = command.Text;
			var info = command.Info;

			updateNews.Content = content;
			updateNews.Title = info.Title;
			updateNews.Summery = info.Summery;
			updateNews.TitleImage = info.TitleImage;
			updateNews.NewsType = info.NewsType;
			updateNews.IsPublished = info.IsPublished;
			updateNews.IsActive = info.IsActive;
			updateNews.ExpirationTime = info.ExpirationTime;
			updateNews.ExpireDuration = info.ExpireDuration;
			updateNews.ScopeId = info.ScopeId;

			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}


		public async Task<UpdateNewsResponse> Handle(UpdateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.Info.NewsID, cancellationToken);
			
		
			var content = updateNews.Content as BottomImageContent;
			content.CopyMap(command);


			var info = command.Info;

			updateNews.CopyMap(info);

			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}

		public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.Info.NewsID, cancellationToken);

			var content = updateNews.Content as TopBottomImageContent;
			content.CopyMap(command);
			

			var info = command.Info;

			updateNews.CopyMap(info);

			
			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}
	}

	public static class InlineMapper
	{
		public static void CopyMap(this object src, object from)
		{
			var srcProperties = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var fromProperties = from.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var prop in srcProperties)
			{
				var fromProperty = fromProperties.FirstOrDefault(i => i.Name == prop.Name);
				if (fromProperty != null)
				{
					var fromPropertyValue = fromProperty.GetValue(from);
					prop.SetValue(src, fromPropertyValue);
				}
			}
		}
	}

}
