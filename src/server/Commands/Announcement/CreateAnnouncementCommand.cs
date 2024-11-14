﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;
using Newtonsoft.Json;
using Shared.Types;

namespace Commands.Announcement
{
	public class CreateAnnouncementCommand: Command<CreateAnnouncementResponse>
    {
        public CreateAnnouncementCommand() { }

        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public AttachedFile TitleImage { get; set; }
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


public class PublishAnnouncementCommand : Command
{
    public Guid AnnouncementId { get; set; }
    public bool Published { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }
}


public class ArchiveAnnouncementCommand : Command
{
    public Guid AnnouncementId { get; set; }
    public bool Archived { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
}

