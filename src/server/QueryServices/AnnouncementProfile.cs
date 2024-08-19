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
			CreateMap<Announcement, AnnouncementDto>()
				.ForMember(d => d.Files, o => o.MapFrom(s => s.Files)); 
				
			
			CreateMap<AnnouncementFiles, AnnouncementFilesDTO>().ForMember(d => d.Files, o => o.MapFrom(s => s.FilesName));

		}
		
	}
}
