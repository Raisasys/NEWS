using Commands.News;
using Core;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Types;

namespace Commands.Announcement
{
	public class UpdateAnnouncementCommand : Command<UpdateAnnouncementResponse>
	{
		public Guid AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public AttachedFile TitleImage { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int ExpireDuration { get; set; }
        public string OwnerScopeId { get; set; }
        public List<AnnouncementFileItem> Files { get; set; }
    }


	public class UpdateAnnouncementResponse()
	{

	}

	public class UpdateIsGlobalAnnounceCommand : Command
	{
		public Guid Id { get; set; }
		public bool IsGlobal { get; set; }
	}


	public class UpdateHaveAccessAnnounceCommand : Command
	{
		public Guid Id { get; set; }
		public List<string> Scopes { get; set; }
		public List<string> Users { get; set; }
	}


    public class AttachCommunicationToAnnounceCommand : Command
    {
        public Guid Id { get; set; }
        public CommunicationMessage Message{ get; set; }
    }


}
