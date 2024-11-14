using Core;
using Domain;
using Shared.Types;

namespace Queries;

public class NewsSimpleDto : IDto
{
	public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summery { get; set; }
    public AttachedFile TitleImage { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int ExpireDuration { get; set; }
    public string OwnerScopeId { get; set; }
    public bool ShouldAuthenticated { get; set; }
    public ArchiveInfo Archived { get; set; }
    public PublishInfo Published { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string NewsType { get; set; }
    

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
	public AttachedFile MainImage { get; set; }
}

public class TopImageContentDto : NewsContentDto
{
	public AttachedFile Image { get; set; }
	public string Text { get; set; }
}

public class VideoContentDto : NewsContentDto
{
    public AttachedFile Video { get; set; }
    public string Text { get; set; }
}
public class BottomImageContentDto : NewsContentDto
{
	public AttachedFile Image { get; set; }
	public string Text { get; set; }
}

public class TopBottomImageContentDto : NewsContentDto
{
	public AttachedFile Image { get; set; }
	public AttachedFile BottomImage { get; set; }
	public string Text { get; set; }
}

public class SliderImagesContentDto : NewsContentDto
{
	public string Text { get; set; }
	public IEnumerable<SliderImageItemDto> SliderImageItemDto { get; set; }
}
public class SliderImageItemDto: IDto
{
	public AttachedFile Image { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public double OrderRank { get; set; }
}
