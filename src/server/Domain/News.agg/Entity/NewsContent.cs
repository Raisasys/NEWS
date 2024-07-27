using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Domain
{
    public abstract class NewsContent : GuidEntity
    {
        [Key]
        public long ContentId { get; set; }
    }

    public class TopImageContent : NewsContent
    {
        public string Image { get; set; }
        public string Text { get; set; }
    }

    public class BottomImageContent : NewsContent
    {
        public string Image { get; set; }
        public string Text { get; set; }
    }

    public class TopBottomImageContent : NewsContent
    {
        public string TopImage { get; set; }
        public string BottomImage { get; set; }
        public string Text { get; set; }
    }

    public class FreeNewsContent : NewsContent
    {
	    public string Content { get; set; }
	    public virtual ICollection<ContentFile> Files { get; set; }
    }

    public class ContentFile : GuidEntity
    {
	    public string FileName { get; set; }
    }

}
