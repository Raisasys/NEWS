using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;

namespace Commands.Announcement
{
	public class CreateAnnouncementCommand: Command<CreateAnnouncementResponse>
    {
        public CreateAnnouncementCommand() { }

		public FileImage Image { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
        public FileImage TitleImage { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int ExpireDuration { get; set; }
        public string OwnerScopeId { get; set; }
        public bool ShouldAuthenticated { get; set; }
        public List<CreateAnnouncementFilesCommand> Files { get; set; }
    }
	
	public class CreateAnnouncementFilesCommand
	{
        public string Name { get; set; }
        public string File { get; set; }

	}

    public class CreateAnnouncementResponse
    {
	    public Guid Id { get; set; }
    }
}
