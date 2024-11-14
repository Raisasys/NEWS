using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Domain;
using Newtonsoft.Json;
using Shared.Types;

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
		public AttachedFile Image { get; set; }
		public string Text { get; set; }
		public bool ShouldAuthenticated { get; set; }
	}


    public class CreateVideoContentCommand : Command<CreateNewsResponse>, ICreateNewsCommand
    {
        public AttachedFile Video { get; set; }
        public string Description { get; set; }
		public NewInfo Info { get; set; }
        public bool ShouldAuthenticated { get; set; }
    }

    public class CreateNewsByBottomImageContentCommand : CreateNewsBaseCommand
	{
	}   public class CreateNewsByTopImageContentCommand : CreateNewsBaseCommand
	{
	}

	public class CreateNewsBySliderImageContentCommand : CreateNewsBaseCommand
	{
		public  IEnumerable<SliderImageItemCommand> SliderImageItemsCommand { get; set; }
	}

	public class SliderImageItemCommand 
	{
		public AttachedFile Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

	}

	public class CreateNewsByTopBottomImageContentCommand : Command<CreateNewsResponse>, ICreateNewsCommand
	{
		public NewInfo Info { get; set; }
		public AttachedFile Image { get; set; }
		public AttachedFile BottomImage { get; set; }
		public string Text { get; set; }
		public bool ShouldAuthenticated { get; set; }
	}


	public class NewInfo
	{
		public string Title { get; set; }
		public string Summery { get; set; }
		public AttachedFile TitleImage { get; set; }
		public DateTime? ExpirationTime { get; set; }
		public int ExpireDuration { get; set; }
		public string ScopeId  { get; set; }
	}



	public class FreeNewsContentCommand : Command<CreateNewsResponse>, ICreateNewsCommand
	{
		public NewInfo Info { get; set; }
		public string Content { get; set; }
		public IEnumerable<AttachedFile> Files { get; set; }
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



    public class PublishNewsCommand : Command
    {
        public Guid NewsId { get; set; }
        public bool Published { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }


    public class ArchiveNewsCommand : Command
    {
        public Guid NewsId { get; set; }
        public bool Archived { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }

}
