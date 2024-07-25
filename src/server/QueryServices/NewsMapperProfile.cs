﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Queries;
using NewsContent = Domain.NewsContent;

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
				.Include<TopImageContent, TopImageContentDto>();
				

		}

	}
}