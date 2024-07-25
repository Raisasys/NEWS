namespace Core;

public interface ITrackable
{
    byte[] RowVersion { get; set; }
}