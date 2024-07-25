using Domain;

namespace Dtos;

public class DashboardCustomerCountsDto
{
    public int All { get; set; }
    public int VerifiedEmail { get; set; }
    public int UnVerifiedEmail { get; set; }
    public int HasActiveSubscription { get; set; }
    public int Disabled { get; set; }
}

public class DashboardLoginActivitiesDto
{
    public int All { get; set; }
    public int CurrentDayLogins { get; set; }
    public int CurrentWeekLogins { get; set; }
    public int CurrentMonthLogins { get; set; }
    public int NothingLogins { get; set; }
}

public class DashboardRequestedConversationDetailsDto
{
    public AIChatDto AIChat { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset? LastUsedAt{ get; set; }
    public int RequestedCount { get; set; }
    public int CurrentDayRequestedCount { get; set; }
    public int CurrentWeekRequestedCount { get; set; }
    public int CurrentMonthRequestedCount { get; set; }
}
