using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Commands.News
{
	public interface IUpdateNewsCommand
	{
		NewInfoCommand Info { get; set; }
		IEnumerable<Guid> Scopes { get; set; }
		bool Authenticated { get; set; }
	}

	public abstract class UpdateNewsCommand : Command<UpdateNewsResponse>, IUpdateNewsCommand
	{
		public NewInfoCommand Info { get; set; }
		public long NewsID { get; set; }
		public string Image { get; set; }
		public string Text { get; set; }
		public bool Authenticated { get; set; }
		public IEnumerable<Guid> Scopes { get; set; }
	}

	public class UpdateNewsBySliderImageContentCommand : UpdateNewsCommand
	{
		public string Text { get; set; }
		public IEnumerable<UpdateSliderImageItemCommand> SliderImageItemsCommand { get; set; }
	}

	public class UpdateSliderImageItemCommand
	{
		public string Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

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
		public string Title { get; set; }
		public string Summery { get; set; }
		public string TitleImage { get; set; }
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
