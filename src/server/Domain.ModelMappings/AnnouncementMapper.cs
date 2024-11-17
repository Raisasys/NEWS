using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings
{
	public class AnnouncementMapper : CustomEntityMapper<Announcement>
	{
		public override void MapBuilder(EntityTypeBuilder<Announcement> entityBuilder)
		{	
			entityBuilder.HasMany(t => t.Files);
            entityBuilder.OwnsOne(t => t.TitleImage); 
            entityBuilder.OwnsOne(t => t.Archived);
            entityBuilder.OwnsOne(t => t.Published);
        }
	}

    public class AnnouncementFileMapper : CustomEntityMapper<AnnouncementFile>
    {
        public override void MapBuilder(EntityTypeBuilder<AnnouncementFile> entityBuilder)
        {
            entityBuilder.OwnsOne(t => t.File);
        }
    }



    public class GroupAnnouncementMapping : CustomEntityMapper<GroupAnnouncement>
    {
        public override void MapBuilder(EntityTypeBuilder<GroupAnnouncement> entityBuilder)
        {
            entityBuilder.OwnsMany(c => c.Items);
            entityBuilder.OwnsOne(t => t.Archived);
            entityBuilder.OwnsOne(t => t.Published);
        }
    }


}
