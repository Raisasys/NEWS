using Core;

namespace Queries;

public class NewsSimpleDto : IDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Summery { get; set; }
	public string TitleImage { get; set; }
	public string NewsType { get; set; }
	public bool IsPublished { get; set; }
	public bool IsActive { get; set; }
	public bool IsArchived { get; set; }
	public DateTime? ExpirationTime { get; set; }
	public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public string ContentId { get; set; }
    public bool IsGlobal { get; set; }

}



public class NewsFullDto : NewsSimpleDto
{
	public NewsContentDto Content { get; set; }

}


public class NewsListDto : IListDto<NewsSimpleDto>
{
	public IEnumerable<NewsSimpleDto> Items { get; set; }
	public int TotalItems{ get; set; }
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

public class SliderImagesContentDto : NewsContentDto
{
	public string Text { get; set; }
	public IEnumerable<SliderImageItemDto> SliderImageItemDto { get; set; }
}
public class SliderImageItemDto: IDto
{
	public string Image { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public double OrderRank { get; set; }
}

public class FreeNewsContentDto : NewsContentDto
{
	public string Content { get; set; }
	public string FileName { get; set; }

}
