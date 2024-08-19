using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Commands.Announcement
{
	public class CreateAnnouncementCommand: Command<CreateAnnouncementResponse>
    {
        public CreateAnnouncementCommand() { }

		public string Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public List<CreateAnnouncementFilesCommand> Files { get; set; }
	}
	
	public class CreateAnnouncementFilesCommand
	{
		public string Files { get; set; }

	}

    public class CreateAnnouncementResponse
    {
	    public long Id { get; set; }
    }
}
