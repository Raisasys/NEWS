﻿using Commands.News;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Shared.Messages;

namespace CommandHandlers
{
    public class NewsCommandHandler : CommandHandlerBase,
        ICommandHandler<CreateNewsByTopImageContentCommand, CreateNewsResponse>,
        ICommandHandler<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>,
        ICommandHandler<CreateNewsByBottomImageContentCommand, CreateNewsResponse>,
        ICommandHandler<CreateNewsBySliderImageContentCommand, CreateNewsResponse>,
        ICommandHandler<CreateVideoContentCommand, CreateNewsResponse>,
        ICommandHandler<UpdateNewsByTopImageContentCommand, UpdateNewsResponse>,
        ICommandHandler<UpdateNewsByBottomImageContentCommand, UpdateNewsResponse>,
        ICommandHandler<UpdateNewsByTopBottomImageContentCommand, UpdateNewsResponse>,
        ICommandHandler<UpdateNewsBySliderImageContentCommand, UpdateNewsResponse>,
        ICommandHandler<UpdateVideoCommand, UpdateNewsResponse>,
        ICommandHandler<DeleteNewCommand>,
        ICommandHandler<PublishNewsCommand>,
        ICommandHandler<ArchiveNewsCommand>


    {

        private IIntegrationBus _integrationBus;

        public NewsCommandHandler(IIntegrationBus integrationBus)
        {
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

            if (command.Image != null|| command.Info.TitleImage != null)
            {
                var files = new List<string>();
                if (command.Image!= null)
                    files.Add(command.Image.FileId);
                if (command.Info.TitleImage!= null)
                    files.Add(command.Info.TitleImage.FileId);

                var fileServiceResponse = await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
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

            if (command.Image != null || command.BottomImage != null || command.Info.TitleImage != null)
            {
                var files = new List<string>();
                if (command.Image!= null)
                    files.Add(command.Image.FileId);
                if (command.BottomImage!= null)
                    files.Add(command.BottomImage.FileId);
                if (command.Info.TitleImage!= null)
                    files.Add(command.Info.TitleImage.FileId);

                var fileServiceResponse = await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(new PersistFilesIntegrationCommand{FileNames = files}, cancellationToken);
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

            if (command.Image != null || command.Info.TitleImage != null)
            {
                var files = new List<string>();
                if (command.Image!= null)
                    files.Add(command.Image.FileId);
                if (command.Info.TitleImage!= null)
                    files.Add(command.Info.TitleImage.FileId);

                var fileServiceResponse =
                    await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(
                        new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
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
            var content = new SliderImagesContent(command.Text, command.Sliders.Select(s => new SliderImageItem()
            {
                Image = s.Image,
                Title = s.Title,
                Description = s.Description,

            }).ToList());

            var newNews = command.CreateNews(content);
            Database.Add(newNews);

            var files = new List<string>();
            if (command.Image != null) files.Add(command.Image.FileId);
            if (command.Info.TitleImage != null) files.Add(command.Info.TitleImage.FileId);

            foreach (var itemCommand in command.Sliders)
                if (itemCommand.Image != null)
                    files.Add(itemCommand.Image.FileId);

            if (files.Any())
            {
                var fileServiceResponse =
                    await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(
                        new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
                //if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");    
            }


            await Database.SaveChanges(cancellationToken);
            return new CreateNewsResponse()
            {
                NewsId = newNews.Id,
            };
        }

        public async Task<CreateNewsResponse> Handle(CreateVideoContentCommand command, CancellationToken cancellationToken)
        {
            var content = new VideoContent()
            {
                Video = command.Video,
                Text = command.Description
            };

            var newNews = command.CreateNews(content);
            Database.Add(newNews);

            if (command.Video != null)
            {

                var fileServiceResponse =
                    await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
                        new PersistFileIntegrationCommand { FileName = command.Video.FileId }, cancellationToken);
                if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
            }

            await Database.SaveChanges(cancellationToken);
            return new CreateNewsResponse()
            {
                NewsId = newNews.Id,
            };
        }


        public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopImageContentCommand command, CancellationToken cancellationToken)
        {
            var updateNews = Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken).Result;

            var content = updateNews.Content as TopImageContent;

            var oldImage = content.Image;
            var oldTitleImage = updateNews.TitleImage;

            content.CopyMap(command);


            var info = command.Info;

            updateNews.CopyMap(info);


            Database.Update(updateNews);

            var files = new List<string>();

            if (command.Image != null && oldImage.FileId != command.Image.FileId)
            {
                files.Add(command.Image.FileId);
            }

            if (command.Info.TitleImage != null && oldTitleImage.FileId != command.Info.TitleImage.FileId)
            {
                files.Add(command.Info.TitleImage.FileId);
            }

            if (files.Any())
            {
                var fileServiceResponse =
                    await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(
                        new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
                if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
            }

            await Database.SaveChanges(cancellationToken);
            return new UpdateNewsResponse();

        }


        public async Task<UpdateNewsResponse> Handle(UpdateNewsByBottomImageContentCommand command, CancellationToken cancellationToken)
        {
            var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);


            var content = updateNews.Content as BottomImageContent;

            var oldImage = content.Image;
            var oldTitleImage = updateNews.TitleImage;

            content.CopyMap(command);

            var info = command.Info;
            updateNews.TitleImage = command.Image;

            updateNews.CopyMap(info);
            Database.Update(updateNews);

            var files = new List<string>();

            if (command.Image != null && oldImage.FileId != command.Image.FileId)
            {
                files.Add(command.Image.FileId);
            }

            if (command.Info.TitleImage != null && oldTitleImage.FileId != command.Info.TitleImage.FileId)
            {
                files.Add(command.Info.TitleImage.FileId);
            }

            if (files.Any())
            {
                var fileServiceResponse =
                    await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(
                        new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
                if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
            }
            
            await Database.SaveChanges(cancellationToken);
            return new UpdateNewsResponse();

        }

        public async Task<UpdateNewsResponse> Handle(UpdateNewsByTopBottomImageContentCommand command, CancellationToken cancellationToken)
        {
            var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);

            var content = updateNews.Content as TopBottomImageContent;

            var oldTopImage = content.TopImage?.FileId;
            var oldBottomImage = content.BottomImage?.FileId;
            var oldTitleImage = updateNews.TitleImage;


            content.CopyMap(command);

            var info = command.Info;
            updateNews.CopyMap(info);

            Database.Update(updateNews);

            var files = new List<string>();

            if (command.TopImage != null && oldTopImage != command.TopImage.FileId)
            {
                files.Add(command.TopImage.FileId);
            }
            if (command.BottomImage != null && oldBottomImage != command.BottomImage.FileId)
            {
                files.Add(command.TopImage.FileId);
            }

            if (command.Info.TitleImage != null && oldTitleImage.FileId != command.Info.TitleImage.FileId)
            {
                files.Add(command.Info.TitleImage.FileId);
            }

            if (files.Any())
            {
                var fileServiceResponse =
                    await _integrationBus.Send<PersistFilesIntegrationCommand, PersistFilesResponse>(
                        new PersistFilesIntegrationCommand { FileNames = files }, cancellationToken);
                if (!fileServiceResponse.Successed) throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
            }
            
            await Database.SaveChanges(cancellationToken);
            return new UpdateNewsResponse();

        }

        public async Task<UpdateNewsResponse> Handle(UpdateNewsBySliderImageContentCommand command, CancellationToken cancellationToken)
        {

            var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);

            var content = new SliderImagesContent(command.Text, command.SliderImageItemsCommand.Select(s => new SliderImageItem()
            {
                Image = s.Image,
                Title = s.Title,
                Description = s.Description,

            }).ToList());

            content.CopyMap(command);

            var info = command.Info;


            //updateNews.CopyMap(info);
            updateNews.TitleImage = command.SliderImageItemsCommand.Select(t => t.Image).FirstOrDefault();
            updateNews.Title = info.Title;
            updateNews.Content = content;
            updateNews.ExpirationTime = info.ExpirationTime;
            updateNews.ExpireDuration = info.ExpireDuration;
            updateNews.OwnerScopeId = info.OwnerScopeId;
            updateNews.Summery = info.Summery;

            Database.Update(updateNews);

            var files = new List<string>();
            foreach (var item in command.SliderImageItemsCommand)
            {
                if (item.Image != null)
                {
                    var fileServiceResponse =
                        await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
                            new PersistFileIntegrationCommand { FileName = item.Image.FileId }, cancellationToken);
                }
            }

            await Database.SaveChanges(cancellationToken);
            return new UpdateNewsResponse();


        }

        public async Task Handle(DeleteNewCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);

            Database.Remove(item);
            Database.Remove(item.Content);
            await Database.SaveChanges(cancellationToken);
        }

       

        public async Task<UpdateNewsResponse> Handle(UpdateVideoCommand command, CancellationToken cancellationToken)
        {
            var updateNews = await Database.Set<News>().Include(t => t.Content).SingleOrDefaultAsync(t => t.Id == command.NewsId, cancellationToken);


            var content = updateNews.Content as VideoContent;

            var oldVideo = content.Video;

            content.CopyMap(command);

            var info = command.Info;

            updateNews.CopyMap(info);
            Database.Update(updateNews);

            if (command.Video != null && oldVideo.FileId != command.Video?.FileId)
            {

                var fileServiceResponse =
                    await _integrationBus.Send<PersistFileIntegrationCommand, PersistFileResponse>(
                        new PersistFileIntegrationCommand { FileName = command.Video.FileId }, cancellationToken);
                if (!fileServiceResponse.Successed)
                    throw new CoreException("عملیات باگزاری فایل با شکست روبرو شد");
            }
            await Database.SaveChanges(cancellationToken);
            return new UpdateNewsResponse();

        }

        public async Task Handle(PublishNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<News>(command.NewsId, cancellationToken);

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

        public async Task Handle(ArchiveNewsCommand command, CancellationToken cancellationToken)
        {
            var item = await Database.Find<News>(command.NewsId, cancellationToken);
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

        return new News(info.Title, info.Summery, content, info.TitleImage, info.ExpirationTime, info.ExpireDuration, info.OwnerScopeId)
        {
        };
    }

}


