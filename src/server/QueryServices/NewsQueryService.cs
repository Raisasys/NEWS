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
		IQueryService<GetNewsListByPagesDto, NewsListDto>,
		IQueryService<GetMySliderImageNewsById, NewsListDto>,
        IQueryService<GetMyNewsById, NewsFullDto>,
        IQueryService<GetMyGroupNewsById, NewsListDto>

    {
		
		private readonly IMapper _mapper;

		public NewsQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<NewsFullDto> Execute(GetNewsById query, CancellationToken cancellationToken)
		{
            var dataById = await Database.Set<News>().Include(c => c.Content).SingleOrDefaultAsync(i => i.Id == query.NewsId, cancellationToken: cancellationToken);
            var result = _mapper.Map<News, NewsFullDto>(dataById);
            return result;
        }


		public async Task<NewsListDto> Execute(GetNewsListDto query, CancellationToken cancellationToken)
		{
			try
			{
				var items = await Database.Set<News>().Include(c => c.Content).Where(t => t.IsDeleted == false && t.IsActive).ToListAsync(cancellationToken: cancellationToken);
				var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);

				return new NewsListDto
				{
					Items = dtos
				};

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}


		public async Task<NewsListDto> Execute(GetArchivedNewsListDto query, CancellationToken cancellationToken)
		{
			try
			{
				var items = await Database.Set<News>().Include(c => c.Content).Where(t => !t.IsActive)
					.ToListAsync(cancellationToken: cancellationToken);
				var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);

				return new NewsListDto
				{
					Items = dtos
				};
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task<NewsListDto> Execute(GetNewsListByPagesDto query, CancellationToken cancellationToken)
		{
			try
			{
				var dataById = await Database.Set<News>().Include(c => c.Content)
					.Where(t => t.IsDeleted == false && t.IsActive).ToListAsync(cancellationToken: cancellationToken);
				var data = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(dataById);

				var offset = (query.PageNumber - 1) * query.PageSize;

				var result = data.Skip(offset).Take(query.PageSize);

				return new NewsListDto
				{
					Items = result,
					TotalItems = data.Count()
				};
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task<NewsListDto> Execute(GetMySliderImageNewsById query, CancellationToken cancellationToken)
		{
			var dataById = await Database.Set<News>().Include(c => c.Content).Include(t=>t.AccessEntityItems.Where(r=> query.ScopeIds.Contains(r.ScopeId)))
				.Where(t => t.IsDeleted == false && t.IsActive).ToListAsync(cancellationToken: cancellationToken);
			var data = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(dataById);

			var offset = (query.PageNumber - 1) * query.PageSize;

			var result = data.Skip(offset).Take(query.PageSize);

			return new NewsListDto
			{
				Items = result,
				TotalItems = data.Count()
			};
		}

        public async Task<NewsFullDto> Execute(GetMyNewsById query, CancellationToken cancellationToken)
        {
            var news = await Database.Set<News>()
                .Include(c => c.Content).Include(c=>c.AccessEntityItems)
                .SingleOrDefaultAsync(i => i.Id == query.NewsId, cancellationToken: cancellationToken);
            
            if (news != null && news.HasAccess(CurrentAppContext.UserIdentity()))
                return _mapper.Map<News, NewsFullDto>(news);
            return null;
        }

        public async Task<NewsListDto> Execute(GetMyGroupNewsById query, CancellationToken cancellationToken)
        {
            var groupNews = await Database.Set<GroupNews>()
                .Include(c => c.Items).Include(c => c.AccessEntityItems)
                .SingleOrDefaultAsync(i => i.Id == query.GroupNewsId, cancellationToken: cancellationToken);

            if (groupNews != null && groupNews.HasAccess(CurrentAppContext.UserIdentity()))
            {
                var items = await Database.Set<News>().Where(c => groupNews.Items.Select(v => v.NewsId).Contains(c.Id)).ToListAsync(cancellationToken);
                var dtos = _mapper.Map<IList<News>, IList<NewsSimpleDto>>(items);
                return new NewsListDto
                {
                    Items = dtos
                };
            }

            return null;
        }

    }
    
}
