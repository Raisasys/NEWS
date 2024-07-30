using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices
{
	public class NewsQueryService : QueryService, 
		IQueryService<GetNewsById, NewsSimpleDto>,
		IQueryService<GetNewsListDto, NewsListDto>
	{

		private readonly IMapper _mapper;

		public NewsQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<NewsSimpleDto> Execute(GetNewsById query, CancellationToken cancellationToken)
		{
			var dataById = await Database.Find<News>(query.NewsId);
			var result = _mapper.Map<News,NewsSimpleDto>(dataById);

			return result;

		}


		public async Task<NewsListDto> Execute(GetNewsListDto query, CancellationToken cancellationToken)
		{
			var items = await Database.Set<News>().Include(c => c.Content).ToListAsync(cancellationToken: cancellationToken);
			var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);
			return new NewsListDto
			{
				Items = dtos
			};
		}



	}
}
