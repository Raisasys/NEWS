using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Queries;

namespace QueryServices
{
    public class NewsMapperProfile : Profile
    {
        public NewsMapperProfile()
        {
            CreateMap<News, NewsSimpleDto>()
	            .ForMember(d=>d.NewsType, o=>o.MapFrom(s=>s.Content.GetType().Name));

            CreateMap<News, NewsSimpleDto>()
                .ForMember(d => d.NewsType, o => o.MapFrom(s => s.Content.GetType().Name));

			CreateMap<NewsContent, NewsContentDto>()
                .Include<TopBottomImageContent, TopBottomImageContentDto>()
                .Include<BottomImageContent, BottomImageContentDto>()
                .Include<TopImageContent, TopImageContentDto>()
                .Include<SliderImagesContent, SliderImagesContentDto>()
                .Include<VideoContent, VideoContentDto>()

			/*.Include<FreeNewsContent, FreeNewsContentDto>()*/
			;

			CreateMap<TopBottomImageContent, TopBottomImageContentDto>()
				 .ForMember(d => d.Image, o => o.MapFrom(s => s.TopImage ));
            CreateMap<BottomImageContent, BottomImageContentDto>();
            CreateMap<TopImageContent, TopImageContentDto>();
            CreateMap<VideoContent, VideoContentDto>();
            CreateMap<News, NewsFullDto>()
				.ForMember(d => d.NewsType, o => o.MapFrom(s => s.Content.GetType().Name));

            CreateMap<SliderImagesContent, SliderImagesContentDto>()
	            .ForMember(x => x.Sliders, d => d.MapFrom(s => s.SliderImageItems));

			CreateMap<SliderImageItem, SliderImageItemDto>();

        }

    }
}
