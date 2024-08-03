using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings
{
    public class NewsDestinationMapping : CustomEntityMapper<NewsDestination>
	{
		public override void MapBuilder(EntityTypeBuilder<NewsDestination> entityBuilder)
		{
		}


	}
	public class NewsDestinationInheritanceMapper : InheritanceEntityMapper<NewsDestination, NewsPublicDestination, NewsScopeDestination>
	{

	}
}
