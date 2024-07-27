using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings
{
    public class NewsContentMapping : CustomEntityMapper<NewsContent>
	{
		public override void MapBuilder(EntityTypeBuilder<NewsContent> entityBuilder)
		{
			entityBuilder.HasKey(t => t.ContentId);
			entityBuilder.Property(t => t.CreatedBy);
			entityBuilder.Property(t => t.CreatedAt);
			entityBuilder.Property(t => t.LastModifiedBy);
			entityBuilder.Property(t => t.LastModifiedAt);

		}


	}

	public class ContentInheritanceMapper : InheritanceEntityMapper<NewsContent, TopBottomImageContent, TopImageContent, BottomImageContent,FreeNewsContent>
	{

	}
}
