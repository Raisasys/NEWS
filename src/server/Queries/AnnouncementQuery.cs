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
		public Guid AnnouncementId { get; set; }
	}

	public class GetAnnouncementListDto : IQuery<AnnouncementListDto>
	{
	}

	public class GetAnnounceHaveAccessQuery : IQuery<HaveAccessDto>
	{
		public Guid Id { get; set; }
	}



    public class GetAnnounceHaveCommunicationsQuery : IQuery<CommunicationItemListDto>
    {
        public Guid Id { get; set; }
    }

    public class GetMyAnnounceById : IQuery<AnnouncementDto>
    {
        public Guid AnnouncementId { get; set; }
    }

}
