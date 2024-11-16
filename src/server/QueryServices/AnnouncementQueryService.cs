using AutoMapper;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryServices
{
	public class AnnouncementQueryService : QueryService, 
		IQueryService<GetAnnouncementById, AnnouncementFullDto>,
		IQueryService<GetAnnouncementListDto, AnnouncementListDto>,
		IQueryService<GetMyAnnounceById, AnnouncementFullDto>
	{
		private readonly IMapper _mapper;
		public AnnouncementQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<AnnouncementFullDto> Execute(GetAnnouncementById query, CancellationToken cancellationToken)
		{
            var dataById = await Database.Set<Announcement>().Include(c => c.Files).SingleOrDefaultAsync(i => i.Id == query.AnnouncementId, cancellationToken: cancellationToken);
            var result = _mapper.Map<Announcement, AnnouncementFullDto>(dataById);
            return result;
        }

		public async Task<AnnouncementListDto> Execute(GetAnnouncementListDto query, CancellationToken cancellationToken)
		{
            var items = await Database.Set<Announcement>().ToListAsync(cancellationToken: cancellationToken);
            var dtos = _mapper.Map<IList<Announcement>, IList<AnnouncementDto>>(items);

            return new AnnouncementListDto
            {
                Items = dtos
            };
        }

        public async Task<AnnouncementFullDto> Execute(GetMyAnnounceById query, CancellationToken cancellationToken)
        {
            var news = await Database.Set<Announcement>().Include(c => c.AccessEntityItems)
                .SingleOrDefaultAsync(i => i.Id == query.AnnouncementId, cancellationToken: cancellationToken);

            if (news != null && news.HasAccess(CurrentAppContext.UserIdentity()))
                return _mapper.Map<Announcement, AnnouncementFullDto>(news);
            return null;
        }
    }
}
