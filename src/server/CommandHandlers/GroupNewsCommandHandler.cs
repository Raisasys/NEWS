using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Commands.GroupNews;
using Commands.News;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommandHandlers
{
    public class GroupNewsCommandHandler : CommandHandlerBase, 
        ICommandHandler<CreateGroupNewsCommand, GroupNewsResponse>,
        ICommandHandler<UpdateGroupNewsCommand, GroupNewsResponse>,
        ICommandHandler<DeleteGroupNewsCommand>,
        ICommandHandler<PublishGroupNewsCommand>,
        ICommandHandler<ArchiveGroupNewsCommand>,
        ICommandHandler<AuthenticatedGroupNewsCommand>

    {
        
        public async Task<GroupNewsResponse> Handle(CreateGroupNewsCommand command, CancellationToken cancellationToken)
        {

            var groupNewsItem = command.Items.Select(t => new GroupNewsItem()
            {
                NewsId = t.NewsId,
                Order = t.Order
            }).ToList();
            var groupNews = new GroupNews(command.Title, command.ExpirationTime, command.ExpireDuration, command.OwnerScopeId, groupNewsItem);
            groupNews.ShouldAuthenticated = command.ShouldAuthenticated;

            Database.Add(groupNews);
            await Database.SaveChanges(cancellationToken);
        
            return new GroupNewsResponse()
            {
                GroupNewsId = groupNews.Id
            };
        }

        public async Task<GroupNewsResponse> Handle(UpdateGroupNewsCommand command, CancellationToken cancellationToken)
        {
            var update = await Database.Set<GroupNews>().Include(t => t.Items).Include(t => t.AccessEntityItems)
                .SingleOrDefaultAsync(t => t.Id == command.GroupNewsId, cancellationToken: cancellationToken);
            var groupNewsItems = command.Items.Select(t => new GroupNewsItem()
            {
                NewsId = t.NewsId,
                Order = t.Order
            }).ToList();

            update.Title = command.Title;
            update.ExpirationTime = command.ExpirationTime;
            update.ExpireDuration = command.ExpireDuration;
            update.OwnerScopeId = command.OwnerScopeId;
            update.ShouldAuthenticated = command.ShouldAuthenticated;
            update.Items = groupNewsItems;

            Database.Update(update);
            await Database.SaveChanges(cancellationToken);

            return new GroupNewsResponse()
            {
                GroupNewsId = update.Id
            };
        }

        public async Task Handle(DeleteGroupNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Set<GroupNews>().Include(t => t.Items).SingleOrDefaultAsync(t => t.Id == command.GroupNewsId, cancellationToken);

            Database.Remove(item);
            await Database.SaveChanges(cancellationToken);
        }


        public async Task Handle(PublishGroupNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupNews>(command.GroupNewsId, cancellationToken);

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

        public async Task Handle(ArchiveGroupNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupNews>(command.GroupNewsId, cancellationToken);
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


        public async Task Handle(AuthenticatedGroupNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<GroupNews>(command.GroupNewsId, cancellationToken);
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
        }
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
