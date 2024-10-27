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
using System.Xml.Linq;

namespace CommandHandlers
{
	public class AnnouncementCommandHandler : CommandHandlerBase,
		ICommandHandler<CreateAnnouncementCommand, CreateAnnouncementResponse>,
		ICommandHandler<UpdateAnnouncementCommand, UpdateAnnouncementResponse>,
		ICommandHandler<DeleteAnnouncementCommand>

	{
		private IIntegrationBus _integrationBus;
		public AnnouncementCommandHandler(IIntegrationBus integrationBus)
		{
			_integrationBus = integrationBus;
		}
		public async Task<CreateAnnouncementResponse> Handle(CreateAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var files = command.Files.Select(s => new AnnouncementFile
			{
				File= s.File,
				Name = s.Name
			}).ToList();

			var announc = new Announcement(command.Title,command.Header, command.Image, command.Description, files);
			Database.Add(announc);

			if (command.Files.Any())
			{
				if (command.Image != null)
				{
					var imageServiceResponse =
						await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
							new PersistFileIntegrationCommand
                            {
                                FileName = command.Image.FileId
                            }, cancellationToken);
					if (!imageServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
				}
			
				foreach (var item in command.Files)
				{
					var fileServiceResponse =
						await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
							new PersistFileIntegrationCommand { FileName = item.File }, cancellationToken);
					if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");

				}

			}

			await Database.SaveChanges(cancellationToken);

			return new CreateAnnouncementResponse()
			{
				Id = announc.Id,
			};

		}


		public async Task<UpdateAnnouncementResponse> Handle(UpdateAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var updateAnnounce = await Database.Set<Announcement>().Include(t => t.Files).SingleOrDefaultAsync(t => t.Id == command.AnnouncementId, cancellationToken);

			foreach (var item in updateAnnounce.Files)
			{
				updateAnnounce.Files.Remove(item);
			}

			var files = command.UpdatedFiles.Select(t => new AnnouncementFile
            {
				File= t.File,
                Name = t.Name
            }).ToList();

			updateAnnounce.Image = command.Image;
			updateAnnounce.Description = command.Description;
			updateAnnounce.Title = command.Title;
			updateAnnounce.Files = files;

			await Database.SaveChanges(cancellationToken);

			return new UpdateAnnouncementResponse();

		}

		public async Task Handle(DeleteAnnouncementCommand command, CancellationToken cancellationToken)
		{
			var updateAnnounce = await Database.Set<Announcement>().Include(t => t.Files).SingleOrDefaultAsync(t => t.Id == command.AnnouncementId, cancellationToken);
			Database.Remove(updateAnnounce);
			await Database.SaveChanges(cancellationToken);
		}
	}
}
