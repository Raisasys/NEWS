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
            CreateMap<News, NewsSimpleDto>();

            CreateMap<NewsContent, NewsContentDto>()
                .Include<TopBottomImageContent, TopBottomImageContentDto>()
                .Include<BottomImageContent, BottomImageContentDto>()
                .Include<TopImageContent, TopImageContentDto>()
                .Include<FreeNewsContent, FreeNewsContentDto>();

            CreateMap<TopBottomImageContent, TopBottomImageContentDto>()
				 .ForMember(d => d.Image, o => o.MapFrom(s => s.TopImage ));
            CreateMap<BottomImageContent, BottomImageContentDto>();
            CreateMap<TopImageContent, TopImageContentDto>();
            CreateMap<FreeNewsContent, FreeNewsContentDto>();
            CreateMap<News, NewsFullDto>();


        }

    }
}
