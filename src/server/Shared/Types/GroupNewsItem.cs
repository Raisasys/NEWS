namespace Domain;

public record GroupNewsItem
{
    public Guid NewsId { get; set; }
    public int Order { get; set; }
}