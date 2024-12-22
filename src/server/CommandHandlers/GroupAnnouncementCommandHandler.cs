using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Commands;
using Commands.Announcement;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;

namespace CommandHandlers
{
    public class GroupAnnouncementCommandHandler : CommandHandlerBase, 
        ICommandHandler<CreateGroupAnnouncementCommand, GroupAnnouncementResponse>,
        ICommandHandler<UpdateGroupAnnouncementCommand, GroupAnnouncementResponse>,
        ICommandHandler<DeleteGroupAnnouncementCommand>,
        ICommandHandler<PublishGroupAnnouncementCommand>,
        ICommandHandler<ArchiveGroupAnnouncementCommand>

    {
        
        public async Task<GroupAnnouncementResponse> Handle(CreateGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {

            var groupAnnouncementItem = command.Items.Select(t => new GroupAnnouncementItem()
            {
                AnnouncementId = t.AnnouncementId,
                Order = t.Order
            }).ToList();
            var groupAnnouncement = new GroupAnnouncement(command.Title, command.OwnerScopeId, groupAnnouncementItem);

            Database.Add(groupAnnouncement);
            await Database.SaveChanges(cancellationToken);
        
            return new GroupAnnouncementResponse()
            {
                GroupAnnouncementId = groupAnnouncement.Id
            };
        }

        public async Task<GroupAnnouncementResponse> Handle(UpdateGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var update = await Database.Set<GroupAnnouncement>().Include(t => t.Items).Include(t => t.AccessEntityItems)
                .SingleOrDefaultAsync(t => t.Id == command.GroupAnnouncementId, cancellationToken: cancellationToken);
            var groupAnnouncementItems = command.Items.Select(t => new GroupAnnouncementItem()
            {
                AnnouncementId = t.AnnouncementId,
                Order = t.Order
            }).ToList();

            update.Title = command.Title;
            update.OwnerScopeId = command.OwnerScopeId;
            update.Items = groupAnnouncementItems;

            Database.Update(update);
            await Database.SaveChanges(cancellationToken);

            return new GroupAnnouncementResponse()
            {
                GroupAnnouncementId = update.Id
            };
        }

        public async Task Handle(DeleteGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Set<GroupAnnouncement>().Include(t => t.Items).SingleOrDefaultAsync(t => t.Id == command.GroupAnnouncementId, cancellationToken);

            Database.Remove(item);
            await Database.SaveChanges(cancellationToken);
        }


        public async Task Handle(PublishGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupAnnouncement>(command.GroupAnnouncementId, cancellationToken);

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

        public async Task Handle(ArchiveGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupAnnouncement>(command.GroupAnnouncementId, cancellationToken);
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


        /*public async Task Handle(AuthenticatedGroupAnnouncementCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupAnnouncement>(command.GroupAnnouncementId, cancellationToken);
            if (command.Authenticated)
            {
                item.ShouldAuthenticated = false;
            }
            else
            {
                item.ShouldAuthenticated = true;
            }

            Database.Update(item);
            await Database.SaveChanges(cancellationToken);
        }*/
    }

    /*public static class InlineMapper
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
    }*/
}
