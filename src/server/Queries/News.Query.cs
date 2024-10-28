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
		public Guid NewsId { get; set; }
	}


    public class GetMyNewsById : IQuery<NewsFullDto>
    {
        public Guid NewsId { get; set; }
    }

    public class GetMyGroupNewsById : IQuery<NewsListDto>
    {
        public Guid GroupNewsId { get; set; }
    }

    public class GetNewsByOwnerId : IQuery<NewsSimpleDto>
    {
        public Guid OwnerId { get; set; }
    }

    public class GetNewsListDto : IQuery<NewsListDto>
	{
	}

	public class GetArchivedNewsListDto : IQuery<NewsListDto>
	{
	}

	public class GetNewsListByPagesDto : IQuery<NewsListDto>
	{

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	
	}

	public class GetMySliderImageNewsById : IQuery<NewsListDto>
	{
		public List<string> ScopeIds { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}

	public class GetNewsHaveAccessQuery : IQuery<HaveAccessDto>
	{
		public Guid Id { get; set; }
	}
}
