﻿using Core;

namespace Domain
{
	[AutoGeneratedKey]
    public abstract class NewsContent : GuidEntity
    {
	    public abstract FileImage MainImage { get; }
	}

    [AutoGeneratedKey]
    public class TopImageContent : NewsContent
    {
        public FileImage Image { get; set; }
        public string Text { get; set; }

        public override FileImage MainImage => Image;
    }

    [AutoGeneratedKey]
    public class BottomImageContent : NewsContent
    {
        public FileImage Image { get; set; }
        public string Text { get; set; }
        public override FileImage MainImage => Image;
	}

    [AutoGeneratedKey]
    public class TopBottomImageContent : NewsContent
    {
        public FileImage TopImage { get; set; }
        public FileImage BottomImage { get; set; }
        public string Text { get; set; }
        public override FileImage MainImage => TopImage;
	}
    /*	public class FreeNewsContent : NewsContent
        {
            public string Content { get; set; }

            public override string MainImage => Files.Any() ? Files.First().FileName : null;
        }*/

    [AutoGeneratedKey]
    public class SliderImagesContent : NewsContent
    {
	    protected SliderImagesContent()
	    {
				
	    }
	    public SliderImagesContent(string text, IEnumerable<SliderImageItem> imageItems)
	    {
		    Text = text;
		    var first = imageItems.First();
		    first.OrderRank = 2.5;
		    SliderImageItems = new List<SliderImageItem>{first};

		    foreach (var item in imageItems.Where(i => i.Image != first.Image).ToList())
			    AddImage(item);
	    }

	    public string Text { get; set; }
	    public override FileImage MainImage => SliderImageItems.FirstOrDefault()?.Image;
	    public virtual ICollection<SliderImageItem> SliderImageItems { get; set; }


	    public void AddImage(SliderImageItem item)
	    {
			item.SortAsLast(SliderImageItems.OrderBy(i=>i.OrderRank).Last());
	    }
	}

	
    [AutoGeneratedKey]
	public class SliderImageItem : GuidEntity, ISortable
	{
		public FileImage Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public double OrderRank { get; set; }
	}
}
