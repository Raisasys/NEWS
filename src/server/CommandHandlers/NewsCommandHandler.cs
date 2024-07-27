using Commands;
using Commands.News;
using Core;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandHandlers
{
	public class NewsCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateNewsCommand, CreateNewsResponse>

	{
		private readonly INewsDomainService newsDomainService;
		public NewsCommandHandler(INewsDomainService _newsDomainService)
		{
			newsDomainService = _newsDomainService;
		}

		public async Task<CreateNewsResponse> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
		{
			var news =  newsDomainService.CreateNews(command.Title, command.Summery, command.TitleImage, command.NewsType, command.IsPublished, 
				command.IsActive, command.IsArchived,command.ExpirationTime,command.ExpireDuration, command.Content, command.ScopeId);

			await Database.SaveChanges(cancellationToken);

			return new CreateNewsResponse()
			{
				NewsId = news.Id,
			};

		}
	}
}
