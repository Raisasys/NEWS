using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Commands.Announcement
{
	public  class DeleteAnnouncementCommand : Command
	{
		public Guid AnnouncementId { get; set; }
	}
}

