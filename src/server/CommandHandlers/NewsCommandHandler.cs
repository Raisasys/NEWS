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
using static System.Net.Mime.MediaTypeNames;

namespace CommandHandlers
{
	public class NewsCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateNewsByTopImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>,
		ICommandHandler<CreateNewsByBottomImageContentCommand, CreateNewsResponse>

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
	}

	

}
