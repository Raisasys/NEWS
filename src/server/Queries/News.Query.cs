using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Queries
{
	public class GetNewsById : IQuery<NewsFullDto>
	{
		public long NewsId { get; set; }
	}
    public class GetNewsByOwnerID : IQuery<NewsSimpleDto>
    {
        public long OwnerID { get; set; }
    }

    public class GetNewsListDto : IQuery<NewsListDto>
	{
	}


}
