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
		}
	}

    public class BottomImageContentMapping : CustomEntityMapper<BottomImageContent>
    {
        public override void MapBuilder(EntityTypeBuilder<BottomImageContent> entityBuilder)
        {
            entityBuilder.OwnsOne(c => c.Image);
        }
    }
    public class TopImageContentMapping : CustomEntityMapper<TopImageContent>
    {
        public override void MapBuilder(EntityTypeBuilder<TopImageContent> entityBuilder)
        {
            entityBuilder.OwnsOne(c => c.Image);
        }
    }

    public class VideoContentMapping : CustomEntityMapper<VideoContent>
    {
        public override void MapBuilder(EntityTypeBuilder<VideoContent> entityBuilder)
        {
            entityBuilder.OwnsOne(c => c.Video);
        }
    }

    public class TopBottomImageContentMapping : CustomEntityMapper<TopBottomImageContent>
    {
        public override void MapBuilder(EntityTypeBuilder<TopBottomImageContent> entityBuilder)
        {
            entityBuilder.OwnsOne(c => c.BottomImage);
            entityBuilder.OwnsOne(c => c.TopImage);
        }
    }

    public class SliderImagesContentMapping : CustomEntityMapper<SliderImagesContent>
	{
		public override void MapBuilder(EntityTypeBuilder<SliderImagesContent> builder)
		{
			builder.HasMany(c => c.SliderImageItems);
			builder.Navigation(item => item.SliderImageItems).AutoInclude(true);
		}
	}
	public class SliderImageItemMapping : CustomEntityMapper<SliderImageItem>
	{
		public override void MapBuilder(EntityTypeBuilder<SliderImageItem> entityBuilder)
        {
            entityBuilder.OwnsOne(c => c.Image);
        }
	}
	

	public class ContentInheritanceMapper : InheritanceEntityMapper<NewsContent, TopBottomImageContent, TopImageContent, BottomImageContent,/*FreeNewsContent,*/SliderImagesContent>
	{

	}
}
