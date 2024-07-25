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
			entityBuilder.HasKey(t => t.NewsId);
			entityBuilder.Property(t => t.ExpirationTime);
			entityBuilder.Property(t => t.ExpireDuration);
			entityBuilder.Property(t => t.IsActive);
			entityBuilder.Property(t => t.IsArchived);
			entityBuilder.Property(t => t.IsPublished);
			entityBuilder.Property(t => t.NewsType);
			entityBuilder.Property(t => t.ScopeId);
			entityBuilder.Property(t => t.Summery);
			entityBuilder.Property(t => t.Title);
			entityBuilder.Property(t => t.TitleImage);
			entityBuilder.Property(t => t.CreatedBy);
			entityBuilder.Property(t => t.CreatedAt);
			entityBuilder.Property(t => t.LastModifiedBy);
			entityBuilder.Property(t => t.LastModifiedAt);
			entityBuilder.HasOne(t => t.Content).WithOne().OnDelete(DeleteBehavior.Cascade);
		}
	}
}

