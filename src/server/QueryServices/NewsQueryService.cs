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
		IQueryService<GetNewsById, NewsFullDto>,
		IQueryService<GetNewsListDto, NewsListDto>,
		IQueryService<GetArchivedNewsListDto, NewsListDto>,
		IQueryService<GetNewsListByPagesDto, NewsListDto>

	{
		
		private readonly IMapper _mapper;

		public NewsQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<NewsFullDto> Execute(GetNewsById query, CancellationToken cancellationToken)
		{
			var dataById = await Database.Set<News>().Include(c => c.Content).Include(c => c.Destination).SingleOrDefaultAsync(i => i.Id == query.NewsId, cancellationToken: cancellationToken);
			var result = _mapper.Map<News, NewsFullDto>(dataById);
			return result;

		}


		public async Task<NewsListDto> Execute(GetNewsListDto query, CancellationToken cancellationToken)
		{
			var items = await Database.Set<News>().Include(c => c.Content).Include(c=>c.Destination).Where(t => t.IsDeleted == false && t.IsActive).ToListAsync(cancellationToken: cancellationToken);
			var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);
		
			return new NewsListDto
			{
				Items = dtos
			};
		}


		public async Task<NewsListDto> Execute(GetArchivedNewsListDto query, CancellationToken cancellationToken)
		{
			var items = await Database.Set<News>().Include(c => c.Content).Where(t => !t.IsActive).ToListAsync(cancellationToken: cancellationToken);
			var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);

			return new NewsListDto
			{
				Items = dtos
			};
		}

		public async Task<NewsListDto> Execute(GetNewsListByPagesDto query, CancellationToken cancellationToken)
		{
			var dataById = await Database.Set<News>().Include(c => c.Content).Where(t => t.IsDeleted == false && t.IsActive).ToListAsync(cancellationToken: cancellationToken);
			var data = _mapper.Map< IList <News> , IList<NewsSimpleDto>>(dataById);

			var offset = (query.PageNumber-1)* query.PageSize;
			
			var result = data.Skip(offset).Take(query.PageSize);
			
			return new NewsListDto
			{
				Items = result
			};
		}
	}
}
