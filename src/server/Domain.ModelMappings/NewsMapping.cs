using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings
{
	public class NewsMapping : CustomEntityMapper<News>
	{
		public override void MapBuilder(EntityTypeBuilder<News> entityBuilder)
		{
			entityBuilder.Property(t => t.ExpirationTime);
			entityBuilder.Property(t => t.ExpireDuration);
			entityBuilder.Property(t => t.OwnerScopeId);
			entityBuilder.Property(t => t.Summery);
			entityBuilder.Property(t => t.Title);
			entityBuilder.OwnsOne(t => t.TitleImage);
			entityBuilder.Property(t => t.CreatedBy);
			entityBuilder.Property(t => t.CreatedAt);
			entityBuilder.Property(t => t.LastModifiedBy);
			entityBuilder.Property(t => t.LastModifiedAt);
			entityBuilder.HasOne(t => t.Content);//.WithOne().OnDelete(DeleteBehavior.Cascade);
            entityBuilder.OwnsOne(t => t.Archived);
            entityBuilder.OwnsOne(t => t.Published);
        }
	}

    public class GroupNewsMapping : CustomEntityMapper<GroupNews>
    {
        public override void MapBuilder(EntityTypeBuilder<GroupNews> entityBuilder)
        {
            entityBuilder.OwnsMany(c => c.Items);
            entityBuilder.OwnsOne(t => t.Archived);
            entityBuilder.OwnsOne(t => t.Published);
        }
    }
    
}

