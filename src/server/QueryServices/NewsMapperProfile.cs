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
	            .ForMember(d=>d.NewsType, o=>o.MapFrom(s=>s.Content.GetType().Name))
				.ForMember(d => d.TitleImage, o => o.MapFrom(s => s.Content.MainImage));

            CreateMap<News, NewsSimpleDto>()
                .ForMember(d => d.NewsType, o => o.MapFrom(s => s.Content.GetType().Name));

			CreateMap<NewsContent, NewsContentDto>()
                .Include<TopBottomImageContent, TopBottomImageContentDto>()
                .Include<BottomImageContent, BottomImageContentDto>()
                .Include<TopImageContent, TopImageContentDto>()
                .Include<SliderImagesContent, SliderImagesContentDto>()

			/*.Include<FreeNewsContent, FreeNewsContentDto>()*/
			;

			CreateMap<TopBottomImageContent, TopBottomImageContentDto>()
				 .ForMember(d => d.Image, o => o.MapFrom(s => s.TopImage ));
            CreateMap<BottomImageContent, BottomImageContentDto>();
            CreateMap<TopImageContent, TopImageContentDto>();
            //CreateMap<FreeNewsContent, FreeNewsContentDto>();
            CreateMap<News, NewsFullDto>()
				.ForMember(d => d.NewsType, o => o.MapFrom(s => s.Content.GetType().Name));

            CreateMap<SliderImagesContent, SliderImagesContentDto>()
	            .ForMember(x => x.MainImage, d=>d.MapFrom(s=>s.SliderImageItems.Select(t=>t.Image).FirstOrDefault()))
	            .ForMember(x => x.Text, d=>d.MapFrom(s=>s.Text))
	            .ForMember(x => x.SliderImageItemDto, d => d.MapFrom(s => s.SliderImageItems));

			CreateMap<SliderImageItem, SliderImageItemDto>();

        }

    }
}
