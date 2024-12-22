using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;
using Shared.Types;

namespace Queries
{
	public class AnnouncementDto : IDto
	{
		public Guid Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public AttachedFile TitleImage { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int ExpireDuration { get; set; }
        public string OwnerScopeId { get; set; }
        public ArchiveInfo Archived { get; set; }
        public PublishInfo Published { get; set; }
	}


    public class AnnouncementFullDto : AnnouncementDto
    {
        public IEnumerable<AnnouncementFileDto> Files { get; set; }
    }

    public class AnnouncementFileDto : IDto
    {
        public string Title { get; set; }
        public AttachedFile File { get; set; }
    }

	public class AnnouncementListDto : IListDto<AnnouncementDto>
	{
		public IEnumerable<AnnouncementDto> Items { get; set; }
	}

}

