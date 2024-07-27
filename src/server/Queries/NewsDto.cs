using Core;
using Domain;

namespace Queries;

public class NewsSimpleDto : IDto
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
	public Guid ScopeId { get; set; }
}


public class NewsListDto : IListDto<NewsSimpleDto>
{
	public IEnumerable<NewsSimpleDto> Items { get; set; }
}

public abstract class NewsContentDto : IDto
{

}

public class TopImageContentDto : NewsContentDto
{
	public string Image { get; set; }
	public string Text { get; set; }
}

public class BottomImageContentDto : NewsContentDto
{
	public string Image { get; set; }
	public string Text { get; set; }
}

public class TopBottomImageContentDto : NewsContentDto
{
	public string TopImage { get; set; }
	public string BottomImage { get; set; }
	public string Text { get; set; }
}

public class FreeNewsContentDto : NewsContentDto
{
    public string Content { get; set; }
    public string FileName { get; set; }
}


