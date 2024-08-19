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
		IQueryService<GetAnnouncementById, AnnouncementDto>,
		IQueryService<GetAnnouncementListDto, AnnouncemenListDto>
	{
		private readonly IMapper _mapper;
		public AnnouncementQueryService(IMapper mapper)
		{
			_mapper = mapper;
		}
		public async Task<AnnouncementDto> Execute(GetAnnouncementById query, CancellationToken cancellationToken)
		{
			try
			{
				var dataById = await Database.Set<Announcement>().Include(c => c.Files).SingleOrDefaultAsync(i => i.Id == query.AnnouncementId, cancellationToken: cancellationToken);
				var result = _mapper.Map<Announcement, AnnouncementDto>(dataById);
				return result;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task<AnnouncemenListDto> Execute(GetAnnouncementListDto query, CancellationToken cancellationToken)
		{
			try
			{
				var items = await Database.Set<Announcement>().Include(c => c.Files).ToListAsync(cancellationToken: cancellationToken);
				var dtos = _mapper.Map<IList<Announcement>, IList<AnnouncementDto>>(items);

				return new AnnouncemenListDto
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
	}
}
