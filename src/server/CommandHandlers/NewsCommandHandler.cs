﻿using Commands;
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
using Shared.Messages;
using System.Reflection.Metadata;

namespace CommandHandlers
{
	public class NewsCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateNewsByTopImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsBySliderImageContentCommand, CreateNewsResponse>,
		ICommandHandler<UpdateNewsByTopImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByBottomImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<UpdateNewsByTopBottomImageContentCommand, UpdateNewsResponse>,
		ICommandHandler<DeleteNewCommand>,
		ICommandHandler<UpdateActivationCommand>


	{
		private readonly INewsDomainService _newsDomainService;

		private IIntegrationBus _integrationBus;

		public NewsCommandHandler(INewsDomainService newsDomainService, IIntegrationBus integrationBus)
		{
			_newsDomainService = newsDomainService;
			_integrationBus = integrationBus;
		}

		public async Task<CreateNewsResponse> Handle(CreateNewsByTopImageContentCommand command, CancellationToken cancellationToken)
		{
			var content = new TopImageContent
			{
				Image = command.Image,
				Text = command.Text
			};

			var newNews = command.CreateNews(content);
			Database.Add(newNews);

			if (!command.Image.IsEmpty())
			{

				var fileServiceResponse =
					await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
						new PersistFileIntegrationCommand { FileName = command.Image }, cancellationToken);
				if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");


			}

			await Database.SaveChanges(cancellationToken);
			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};
		}

		public async Task<CreateNewsResponse> Handle(CreateNewsByTopBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var content = new TopBottomImageContent
			{
				TopImage = command.Image,
				BottomImage = command.BottomImage,
				Text = command.Text
			};

			var newNews = command.CreateNews(content);

			Database.Add(newNews);

			if (!command.Image.IsEmpty())
			{

				var fileServiceResponse =
					await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
						new PersistFileIntegrationCommand { FileName = command.Image }, cancellationToken);
				if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");


			}

			await Database.SaveChanges(cancellationToken);
			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};
		}


		public async Task<CreateNewsResponse> Handle(CreateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var content = new BottomImageContent
			{
				Image = command.Image,
				Text = command.Text
			};

			var newNews = command.CreateNews(content);
			Database.Add(newNews);

			if (!command.Image.IsEmpty())
			{

				var fileServiceResponse =
					await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
						new PersistFileIntegrationCommand { FileName = command.Image }, cancellationToken);
				if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");


			}

			await Database.SaveChanges(cancellationToken);
			return new CreateNewsResponse()
			{
				NewsId = newNews.Id,
			};
		}

		public async Task<CreateNewsResponse> Handle(CreateNewsBySliderImageContentCommand command, CancellationToken cancellationToken)
		{
			try
			{
				var content = new SliderImagesContent(command.Text, command.SliderImageItemsCommand.Select(s => new SliderImageItem()
				{
					Image = s.Image,
					Title = s.Title,
					Description = s.Description,
					
				}).ToList());

				var newNews = command.CreateNews(content);
				Database.Add(newNews);

				if (!command.Image.IsEmpty())
				{

					var fileServiceResponse =
						await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
							new PersistFileIntegrationCommand { FileName = command.Image }, cancellationToken);
					if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");


				}

				await Database.SaveChanges(cancellationToken);
				return new CreateNewsResponse()
				{
					NewsId = newNews.Id,
				};

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}


		public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = Database.Set<News>().Include(t => t.Content).Include(t => t.Destination).SingleOrDefaultAsync(t => t.Id == command.NewsID, cancellationToken).Result;

			var content = updateNews.Content as TopImageContent;
			content.CopyMap(command);

			var destination = updateNews.Destination;
			destination.CopyMap(command);

			var info = command.Info;

			updateNews.CopyMap(info);

			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}


		public async Task<UpdateNewsResponse> Handle(UpdateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = await Database.Set<News>().Include(t => t.Content).Include(t => t.Destination).SingleOrDefaultAsync(t => t.Id == command.NewsID, cancellationToken);


			var content = updateNews.Content as BottomImageContent;
			content.CopyMap(command);
			var destination = updateNews.Destination;
			destination.CopyMap(command);

			var info = command.Info;

			updateNews.CopyMap(info);

			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}

		public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopBottomImageContentCommand command, CancellationToken cancellationToken)
		{
			var updateNews = await Database.Set<News>().Include(t => t.Content).Include(t => t.Destination).SingleOrDefaultAsync(t => t.Id == command.NewsID, cancellationToken);

			var content = updateNews.Content as TopBottomImageContent;
			content.CopyMap(command);

			var destination = updateNews.Destination;
			destination.CopyMap(command);

			var info = command.Info;

			updateNews.CopyMap(info);


			Database.Update(updateNews);
			await Database.SaveChanges(cancellationToken);
			return new UpdateNewsResponse();

		}

		public async Task Handle(DeleteNewCommand command, CancellationToken cancellationToken)
		{
			var item = await Database.Set<News>().Include(t => t.Content).Include(t => t.Destination).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);

			Database.Remove(item);
			Database.Remove(item.Content);
			await Database.SaveChanges(cancellationToken);
		}

		public async Task Handle(UpdateActivationCommand command, CancellationToken cancellationToken)
		{
			var item = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);
			if (item.IsActive)
			{
				item.IsActive = false;
			}
			else
			{
				item.IsActive = true;
			}


			await Database.SaveChanges(cancellationToken);

		}


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

	public static News CreateNews(this ICreateNewsCommand command, NewsContent content)
	{
		var info = command.Info;


		if (command.Scopes != null && command.Scopes.Any())
		{
			return News.Scope(info.Title, info.Summery, content, info.TitleImage, true, true, true, info.ExpirationTime, info.ExpireDuration, info.ScopeId, command.Scopes);
		}

		return News.Public(info.Title, info.Summery, content, info.TitleImage, true, true, true, info.ExpirationTime, info.ExpireDuration, info.ScopeId, command.Authenticated);
	}

}


