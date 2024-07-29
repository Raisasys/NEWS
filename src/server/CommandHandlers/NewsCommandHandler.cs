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

namespace CommandHandlers
{
	public class NewsCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateNewsByTopImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<UpdateNewsByTopImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByBottomImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByTopBottomImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<DeleteNewCommand>

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
			var updateNews = Database.Set<News>().Include(t=>t.Content).SingleOrDefaultAsync(t=>t.Id == command.NewsID, cancellationToken).Result;

			var content = updateNews.Content as TopImageContent;
			content.CopyMap(command);


			var info = command.Info;

			updateNews.CopyMap(info);

			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}


		public async Task<UpdateNewsResponse> Handle(UpdateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsID, cancellationToken);
			
		
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
			var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsID, cancellationToken);

			var content = updateNews.Content as TopBottomImageContent;
			content.CopyMap(command);
			

			var info = command.Info;

			updateNews.CopyMap(info);

			
			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}

		public async Task Handle(DeleteNewCommand command, CancellationToken cancellationToken)
		{
			var item = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);

			Database.Remove(item);
			await Database.SaveChanges(cancellationToken);
		}
	}

	public static class InlineMapper
	{
		public static void CopyMap(this object src, object from)
		{
			var srcProperties = src.GetType().GetProperties();
			var fromProperties = from.GetType().GetProperties();

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
