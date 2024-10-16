﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Queries
{
	public class AnnouncementDto : IDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
		public string Description { get; set; }
        public string TitleImage { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int ExpireDuration { get; set; }
        public string OwnerScopeId { get; set; }
        public bool ShouldAuthenticated { get; set; }
        public virtual IEnumerable<AnnouncementFileDTO> Files { get; set; }
	}

	public class AnnouncementFileDTO : IDto
	{
		public string File { get; set; }
        public string Name { get; set; }
    }

	public class AnnouncementListDto : IListDto<AnnouncementDto>
	{
		public IEnumerable<AnnouncementDto> Items { get; set; }
	}

}

