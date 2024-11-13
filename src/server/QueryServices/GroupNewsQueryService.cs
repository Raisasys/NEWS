using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices
{
	public class GroupNewsQueryService : QueryService, 
		IQueryService<GroupNewsByIdQuery, GroupNewsDto>,
		IQueryService<GetGroupNewsListQuery, GroupNewsListDto>
    {
		
		private readonly IMapper _mapper;

		public GroupNewsQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
        public async Task<GroupNewsDto> Execute(GroupNewsByIdQuery query, CancellationToken cancellationToken)
        {
            var dataById = await Database.Set<GroupNews>().Include(c => c.Items).SingleOrDefaultAsync(i => i.Id == query.GroupNewsId, cancellationToken: cancellationToken);
            var result = _mapper.Map<GroupNews, GroupNewsDto>(dataById);
            return result;
        }


        public async Task<GroupNewsListDto> Execute(GetGroupNewsListQuery query, CancellationToken cancellationToken)
        {
            var items = await Database.Set<GroupNews>().Include(c => c.Items).ToListAsync(cancellationToken: cancellationToken);
            var dtos = _mapper.Map<IList<GroupNews>, IList<GroupNewsDto>>(items);

            return new GroupNewsListDto
            {
                Items = dtos
            };

        }



        
    }
    
}
