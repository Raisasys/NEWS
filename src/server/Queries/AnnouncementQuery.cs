using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
	public class GetAnnouncementById : IQuery<AnnouncementDto>
	{
		public long AnnouncementId { get; set; }
	}

	public class GetAnnouncementListDto : IQuery<AnnouncemenListDto>
	{
	}
}
