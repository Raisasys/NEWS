using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;
using Shared.Types;

namespace Commands.News
{
	public interface IUpdateNewsCommand
	{
		NewInfoCommand Info { get; set; }
	}

	public abstract class UpdateNewsCommand : Command<UpdateNewsResponse>, IUpdateNewsCommand
	{
		public NewInfoCommand Info { get; set; }
		public Guid NewsId { get; set; }
		public AttachedFile Image { get; set; }
		public string Text { get; set; }
	}

	public class UpdateNewsBySliderImageContentCommand : UpdateNewsCommand
	{
		public string Text { get; set; }
		public IEnumerable<UpdateSliderImageItemCommand> SliderImageItemsCommand { get; set; }
	}

	public class UpdateSliderImageItemCommand
	{
		public AttachedFile Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

	}

    public class UpdateVideoCommand : Command<UpdateNewsResponse>, IUpdateNewsCommand
    {
        public AttachedFile Video { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid NewsId { get; set; }
        public NewInfoCommand Info { get; set; }
    }

    public class UpdateNewsByTopImageContentCommand : UpdateNewsCommand
	{

	}

	public class UpdateNewsByBottomImageContentCommand : UpdateNewsCommand
	{
	}

	public class UpdateNewsByTopBottomImageContentCommand : UpdateNewsCommand
	{
		public AttachedFile TopImage { get; set; }
		public AttachedFile BottomImage { get; set; }
		public string Text { get; set; }
	}

	
	public class NewInfoCommand
	{
		public string Title { get; set; }
        public AttachedFile TitleImage { get; set; }
        public string Summery { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public string OwnerScopeId { get; set; }
	}


	public class UpdateNewsResponse()
	{

	}

	public class UpdateIsGlobalNewsCommand : Command
	{
		public Guid Id { get; set; }
		public bool IsGlobal { get; set; }


	}


	public class UpdateHaveAccessNewsCommand : Command
	{
		public Guid Id { get; set; }
		public List<string> Scopes { get; set; }
		public List<string> Users { get; set; }
	}

}
