using Core.EventBus;

namespace Core.Events
{
    public abstract class EventBase : IEventBase
	{
        protected EventBase()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; }
        public abstract bool IsDelayed { get; }
        public string EventKey { get; set; }
        public abstract bool ShouldBeExecute { get; }
        public DateTime CreateAt { get; } = DateTime.Now;
        public abstract string Channel { get; }
	}


    public abstract class Event<TEventBusChannel> : EventBase, IEvent<TEventBusChannel>
	    where TEventBusChannel : IEventBusChannel, new()
    {
	    public override string Channel => typeof(TEventBusChannel).Name;
		public override bool ShouldBeExecute => true;
	    public override bool IsDelayed => false;
    }

	public abstract class DelayedEventBase<TEventBusChannel> : EventBase, IDelayedEvent<TEventBusChannel>
		where TEventBusChannel : IEventBusChannel, new()
	{
		public override string Channel => typeof(TEventBusChannel).Name;
		public override bool ShouldBeExecute => false;
		public override bool IsDelayed => true;
		public abstract TimeSpan Delayed { get; set; }
	}
}
