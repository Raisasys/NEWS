using Core.Events;

namespace Core.EventBus
{
    public interface IEventHandler<TEventBusChannel> 
	    where TEventBusChannel : IEventBusChannel
    {
	    public string Channel => typeof(TEventBusChannel).Name;
    }

    public interface IEventHandle<in TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }


    public interface IDelayedIEventHandle<in TDelayedEvent>
        where TDelayedEvent : IDelayedEvent
    {
        Task Handle(TDelayedEvent @event);
    }


    public abstract class EventHandler<TEventBusChannel> : IEventHandler<TEventBusChannel>
		where TEventBusChannel : IEventBusChannel
    {
        protected IUowDatabase Database => Context.GetDatabase();

    }
}


