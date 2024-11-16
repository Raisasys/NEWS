namespace Domain;

public record GroupNewsItem
{
    public Guid NewsId { get; set; }
    public int Order { get; set; }
}


public record ArchiveInfo(DateTime At, string By)
{
}
public record PublishInfo(DateTime At , string By){}