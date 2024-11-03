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

namespace CommandHandlers
{
    public class GroupNewsCommandHandler : CommandHandlerBase, 
        ICommandHandler<GroupNewsCommand, GroupNewsResponse>,
        ICommandHandler<GroupNewsUpdateCommand, GroupNewsResponse>,
        ICommandHandler<DeleteGroupNewsCommand>
    {
        
        public async Task<GroupNewsResponse> Handle(GroupNewsCommand command, CancellationToken cancellationToken)
        {

            var GroupNewsItem = command.Items.Select(t => new GroupNewsItem()
            {
                NewsId = t.NewsId,
                Order = t.Order
            }).ToList();
            var groupNews = new GroupNews(command.Title, command.Summery, command.IsActive, command.IsArchived, command.ExpirationTime, command.ExpireDuration, command.OwnerScopeId, GroupNewsItem);
            Database.Add(groupNews);
            await Database.SaveChanges(cancellationToken);
        
            return new GroupNewsResponse()
            {
                GroupNewsId = groupNews.Id
            };
        }

        public async Task<GroupNewsResponse> Handle(GroupNewsUpdateCommand command, CancellationToken cancellationToken)
        {
            var update = await Database.Set<GroupNews>().Include(t => t.Items).Include(t => t.AccessEntityItems)
                .SingleOrDefaultAsync(t => t.Id == command.GroupNewsId, cancellationToken: cancellationToken);
            var GroupNewsItem = command.Items.Select(t => new GroupNewsItem()
            {
                NewsId = t.NewsId,
                Order = t.Order
            }).ToList();

            GroupNewsItem.CopyMap(command);

            update.ExpirationTime = command.ExpirationTime;
            update.ExpireDuration = command.ExpireDuration;
            update.IsArchived = command.IsArchived;
            update.OwnerScopeId = command.OwnerScopeId;
            update.ShouldAuthenticated = command.ShouldAuthenticated;
            update.Summery = command.Summery;
            update.Title = command.Title;
            update.Items = GroupNewsItem;
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
