using Commands.News;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Announcement
{
	public class UpdateAnnouncementCommand : Command<UpdateAnnouncResponse>
	{
		public long AnnouncementId { get; set; }
		public string Title { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }

		public IEnumerable<UpdateAnnouncementFilesCommand> UpdatedFiles { get; set; }
	}

	public class UpdateAnnouncementFilesCommand
	{
		public string Files { get; set; }
	}

	public class UpdateAnnouncResponse()
	{

	}
}
