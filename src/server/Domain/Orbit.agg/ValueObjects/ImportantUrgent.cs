using Newtonsoft.Json;

namespace Domain;

public record ImportantUrgent(int Important, int Urgent)
{
    public ImportantUrgent() : this(0,0) { }

    public int Important { get; set; } = Important;
    public int Urgent { get; set; } = Urgent;

    [JsonIgnore]
    public int Total => Important + Urgent;
}