using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Commands.News
{
	

	public abstract class UpdateNewsCommand : Command<UpdateNewsResponse>
	{
		public NewInfoCommand Info { get; set; }
		public string Image { get; set; }
		public string Text { get; set; }
	}

	public class UpdateNewsByTopImageContentCommand : UpdateNewsCommand
	{

	}

	public class UpdateNewsByBottomImageContentCommand : UpdateNewsCommand
	{
	}

	public class UpdateNewsByTopBottomImageContentCommand : UpdateNewsCommand
	{
		public string TopImage { get; set; }
		public string BottomImage { get; set; }
		public string Text { get; set; }
	}


	public class NewInfoCommand
	{
		public long NewsID { get; set; }
		public string Title { get; set; }
		public string Summery { get; set; }
		public string TitleImage { get; set; }
		public byte NewsType { get; set; }
		public bool IsPublished { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public Guid ScopeId { get; set; }
	}


	public class UpdateNewsResponse()
	{

	}

}
