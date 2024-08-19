using Commands.Announcement;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Commands.News;
using Shared.Messages;
using Microsoft.EntityFrameworkCore;

namespace CommandHandlers
{
	public class AnnouncementCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateAnnouncementCommand, CreateAnnouncementResponse>,
		ICommandHandler<UpdateAnnouncementCommand, UpdateAnnouncResponse>,
		ICommandHandler<DeleteAnnouncementCommand>

	{
		private IIntegrationBus _integrationBus;
		public AnnouncementCommandHandler(IIntegrationBus integrationBus)
		{
			_integrationBus = integrationBus;
		}
		public async Task<CreateAnnouncementResponse> Handle(CreateAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var files = command.Files.Select(s => new AnnouncementFiles
			{
				FilesName = s.Files
			}).ToList();

			var announc = new Announcement(command.Title, command.Image, command.Description, files);
			Database.Add(announc);

			if (!command.Image.IsEmpty())
			{

				var imageServiceResponse =
					await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
						new PersistFileIntegrationCommand { FileName = command.Image }, cancellationToken);
				if (!imageServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");

				foreach (var item in command.Files)
				{
					var fileServiceResponse =
						await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
							new PersistFileIntegrationCommand { FileName = item.Files }, cancellationToken);
					if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");

				}

			}

			await Database.SaveChanges(cancellationToken);

			return new CreateAnnouncementResponse()
			{
				Id = announc.Id,
			};

		}


		public async Task<UpdateAnnouncResponse> Handle(UpdateAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var updateAnnounce = await Database.Set<Announcement>().Include(t => t.Files).SingleOrDefaultAsync(t => t.Id == command.AnnouncementId, cancellationToken);

			foreach (var item in updateAnnounce.Files)
			{
				updateAnnounce.Files.Remove(item);
			}

			var files = command.UpdatedFiles.Select(t => new AnnouncementFiles
			{
				FilesName = t.Files
			}).ToList();

			updateAnnounce.Image = command.Image;
			updateAnnounce.Description = command.Description;
			updateAnnounce.Title = command.Title;
			updateAnnounce.Files = files;

			await Database.SaveChanges(cancellationToken);

			return new UpdateAnnouncResponse();

		}

		public async Task Handle(DeleteAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var updateAnnounce = await Database.Set<Announcement>().Include(t => t.Files).SingleOrDefaultAsync(t => t.Id == command.AnnouncementId, cancellationToken);
			Database.Remove(updateAnnounce);
			await Database.SaveChanges(cancellationToken);
		}
	}
}
