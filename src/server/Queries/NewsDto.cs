using Core;
using Domain;

namespace Queries;

public class NewsSimpleDto : IDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Summery { get; set; }
	public string TitleImage { get; set; }
	public string NewsType { get; set; }
	public bool IsPublished { get; set; }
	public bool IsActive { get; set; }
	public bool IsArchived { get; set; }
	public DateTime? ExpirationTime { get; set; }
	public int ExpireDuration { get; set; }
	public Guid ScopeId { get; set; }
    public string OwnerID { get; set; }
    public string ContentID { get; set; }
}



public class NewsFullDto : NewsSimpleDto
{
	public NewsContentDto Content { get; set; }
	public DestinationDto Destination { get; set; }
	
}


public class NewsListDto : IListDto<NewsSimpleDto>
{
	public IEnumerable<NewsSimpleDto> Items { get; set; }
}

public abstract class NewsContentDto : IDto
{
	public string MainImage { get; set; }
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
	public string Image { get; set; }
	public string BottomImage { get; set; }
	public string Text { get; set; }
}

public class FreeNewsContentDto : NewsContentDto
{
    public string Content { get; set; }
    public string FileName { get; set; }

}

public abstract class DestinationDto : IDto
{
}


public class NewsScopeDestinationDto : DestinationDto
{

}

public class NewsPublicDestinationDto : DestinationDto
{
	public bool Authenticated { get; set; }
}

