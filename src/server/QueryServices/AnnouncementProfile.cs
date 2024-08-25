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
	public class AnnouncementProfile : Profile
	{
		public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>();
			CreateMap<AnnouncementFile, AnnouncementFileDTO>();

		}
		
	}
}
