using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;

namespace Commands.News
{
	public interface ICreateNewsCommand
	{
		NewInfo Info { get; set; }
		bool ShouldAuthenticated { get; set; }
	}
	public abstract class CreateNewsBaseCommand : Command<CreateNewsResponse>, ICreateNewsCommand
	{
		public NewInfo Info { get; set; }
		public FileImage Image { get; set; }
		public string Text { get; set; }
		public bool ShouldAuthenticated { get; set; }
	}

	public class CreateNewsByTopImageContentCommand : CreateNewsBaseCommand
	{
	}

	public class CreateNewsByBottomImageContentCommand : CreateNewsBaseCommand
	{
	}

	public class CreateNewsBySliderImageContentCommand : CreateNewsBaseCommand
	{
		public  IEnumerable<SliderImageItemCommand> SliderImageItemsCommand { get; set; }
	}

	public class SliderImageItemCommand 
	{
		public FileImage Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

	}

	public class CreateNewsByTopBottomImageContentCommand : Command<CreateNewsResponse>, ICreateNewsCommand
	{
		public NewInfo Info { get; set; }
		public FileImage Image { get; set; }
		public FileImage BottomImage { get; set; }
		public string Text { get; set; }
		public bool ShouldAuthenticated { get; set; }
	}


	public class NewInfo
	{
		public string Title { get; set; }
		public string Summery { get; set; }
		public FileImage TitleImage { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public string ScopeId  { get; set; }
	}



	public class FreeNewsContentCommand : Command<CreateNewsResponse>, ICreateNewsCommand
	{
		public NewInfo Info { get; set; }
		public string Content { get; set; }
		public IEnumerable<FileImage> Files { get; set; }
		public bool ShouldAuthenticated { get; set; }
	}
	public class CreateNewsResponse
	{
		public Guid NewsId { get; set; }
	}

    public class AttachCommunicationToNewsCommand : Command
    {
        public Guid Id { get; set; }
        public CommunicationMessage Message { get; set; }
    }
}
