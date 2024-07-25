using Core.EventBus;

namespace Events;

public class SmsEventBusChannel : IEventBusChannel
{
	public const string CHANNEL = "SMS";
}

public class EmailEventBusChannel : IEventBusChannel
{
	public const string CHANNEL = "EMAIL";
}