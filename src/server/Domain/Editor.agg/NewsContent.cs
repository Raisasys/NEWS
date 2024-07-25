using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Domain
{
	public abstract class NewsContent: GuidEntity
	{
		
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


}
