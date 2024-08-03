using Core;

namespace Domain
{
    public abstract class NewsContent : LongEntity
    {
	    public abstract string MainImage { get; }
	}



    public class TopImageContent : NewsContent
    {
        public string Image { get; set; }
        public string Text { get; set; }

        public override string MainImage => Image;
    }

    public class BottomImageContent : NewsContent
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public override string MainImage => Image;
	}

    public class TopBottomImageContent : NewsContent
    {
        public string TopImage { get; set; }
        public string BottomImage { get; set; }
        public string Text { get; set; }
        public override string MainImage => TopImage;
	}

    public class FreeNewsContent : NewsContent
    {
	    public string Content { get; set; }
	    public virtual ICollection<ContentFile> Files { get; set; }

	    public override string MainImage => Files.Any() ? Files.First().FileName : null;
	}

    public class ContentFile : GuidEntity
    {
	    public string FileName { get; set; }
    }

}
