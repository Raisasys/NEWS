using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Queries
{
	public class AnnouncementDto : IDto
	{
		public string Title { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public virtual IEnumerable<AnnouncementFilesDTO> Files { get; set; }
	}

	public class AnnouncementFilesDTO: IDto
	{
		public string Files { get; set; }
	}

	public class AnnouncemenListDto : IListDto<AnnouncementDto>
	{
		public IEnumerable<AnnouncementDto> Items { get; set; }
	}

}

