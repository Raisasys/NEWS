using Core.EventBus;

namespace Core.Events
{
    public interface IEventBase
    {
        Guid EventId { get; }
        bool IsDelayed { get; }
        string EventKey { get; }
		bool ShouldBeExecute { get; }
        DateTime CreateAt { get; }
        string Channel { get; }
	}

    public interface IEvent: IEventBase
    {
    }
    public interface IDelayedEvent : IEventBase
    {
	    
	    TimeSpan Delayed { get; }
	}


	public interface IEvent<TEventBusChannel> : IEvent
		where TEventBusChannel : IEventBusChannel
	{
		
	}
	public interface IDelayedEvent<TEventBusChannel> : IDelayedEvent
		where TEventBusChannel : IEventBusChannel
	{
		public string Channel => typeof(TEventBusChannel).Name;
		TimeSpan Delayed { get; }
		string EventKey { get; }
	}
}
