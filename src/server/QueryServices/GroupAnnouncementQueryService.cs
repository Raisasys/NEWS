using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace QueryServices
{
	public class GroupAnnouncementQueryService : QueryService, 
		IQueryService<GroupAnnouncementByIdQuery, GroupAnnouncementDto>,
		IQueryService<GroupAnnouncementListQuery, GroupAnnouncementListDto>
    {
		
		private readonly IMapper _mapper;

		public GroupAnnouncementQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
        public async Task<GroupAnnouncementDto> Execute(GroupAnnouncementByIdQuery query, CancellationToken cancellationToken)
        {
            var dataById = await Database.Set<GroupAnnouncement>().Include(c => c.Items).SingleOrDefaultAsync(i => i.Id == query.GroupAnnouncementId, cancellationToken: cancellationToken);
            var result = _mapper.Map<GroupAnnouncement, GroupAnnouncementDto>(dataById);
            return result;
        }


        public async Task<GroupAnnouncementListDto> Execute(GroupAnnouncementListQuery query, CancellationToken cancellationToken)
        {
            var items = await Database.Set<GroupAnnouncement>().Include(c => c.Items).ToListAsync(cancellationToken: cancellationToken);
            var dtos = _mapper.Map<IList<GroupAnnouncement>, IList<GroupAnnouncementDto>>(items);

            return new GroupAnnouncementListDto
            {
                Items = dtos
            };

        }



        
    }
    
}
