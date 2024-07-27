using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;

namespace Commands.News
{
    public class CreateNewsCommand : Command<CreateNewsResponse>
	{
		public long NewsId { get; set; }
		public string Title { get; set; }
		public string Summery { get; set; }
		public string TitleImage { get; set; }
		public byte NewsType { get; set; }
		public bool IsPublished { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public virtual NewsContent Content { get; set; }
		public Guid ScopeId { get; set; }
	}


	public class CreateNewsResponse
	{
		public long NewsId { get; set; }
	}
}
