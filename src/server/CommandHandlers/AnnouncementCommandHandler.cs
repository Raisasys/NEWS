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
        ICommandHandler<DeleteAnnouncementCommand>,
    ICommandHandler<PublishAnnouncementCommand>,
        ICommandHandler<ArchiveAnnouncementCommand>

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

            var announc = new Announcement(command.Title, command.Header, command.TitleImage, command.Description, files);
            announc.ShouldAuthenticated = command.ShouldAuthenticated;
            announc.ExpirationTime = command.ExpirationTime;
            announc.ExpireDuration = command.ExpireDuration;
            announc.OwnerScopeId= command.OwnerScopeId;

            Database.Add(announc);

            if (command.Files.Any())
            {
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

            updateAnnounce.TitleImage= command.TitleImage;
            updateAnnounce.Description = command.Description;
            updateAnnounce.Title = command.Title;
            updateAnnounce.Files = files;
            updateAnnounce.ShouldAuthenticated = command.ShouldAuthenticated;
            updateAnnounce.ExpirationTime = command.ExpirationTime;
            updateAnnounce.ExpireDuration = command.ExpireDuration;
            updateAnnounce.OwnerScopeId= command.OwnerScopeId;

            await Database.SaveChanges(cancellationToken);

            return new UpdateAnnouncementResponse();

        }

        public async Task Handle(DeleteAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var updateAnnounce = await Database.Set<Announcement>().Include(t => t.Files).SingleOrDefaultAsync(t => t.Id == command.AnnouncementId, cancellationToken);
            Database.Remove(updateAnnounce);
            await Database.SaveChanges(cancellationToken);
        }

        public async Task Handle(PublishAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<Announcement>(command.AnnouncementId, cancellationToken);

            if (command.Published)
            {
                item.Publish(command.UserId);
            }
            else
            {
                item.Published = null;
            }

            Database.Update(item);
            await Database.SaveChanges(cancellationToken);
        }

        public async Task Handle(ArchiveAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<Announcement>(command.AnnouncementId, cancellationToken);
            if (command.Archived)
            {
                item.Archive(command.UserId);
            }
            else
            {
                item.Archived = null;
            }

            Database.Update(item);
            await Database.SaveChanges(cancellationToken);
        }
    }
}
