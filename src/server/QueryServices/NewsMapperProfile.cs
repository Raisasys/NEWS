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

			CreateMap<NewsContent, NewsContentDto>().ReverseMap();
			CreateMap<TopBottomImageContent, TopBottomImageContentDto>().ReverseMap();
			CreateMap<BottomImageContent, BottomImageContentDto>().ReverseMap();
			CreateMap<TopImageContent, TopImageContentDto>().ReverseMap();
				CreateMap<FreeNewsContent, FreeNewsContentDto>().ReverseMap();
				

		}

	}
}
