using Core.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus
{
    public class EventListenerContainer
	{
		internal EventListenerContainer(){}

        internal List<EventHandlerType> EventHandlerTypes { get; set; } = new ();
		
        public EventListenerContainer RegisterEventHandler<TEventBusChannel, TEventListener>()
	        where TEventListener : IEventHandler<TEventBusChannel>
	        where TEventBusChannel : IEventBusChannel
	        => RegisterEventHandler(typeof(TEventBusChannel).Name, typeof(TEventListener));


		internal EventListenerContainer RegisterEventHandler(string channel, Type handlerType)
        {
	        EventHandlerTypes.Add(new EventHandlerType(channel, handlerType));
            return this;
        }

        
        internal void AddEventHandler(IServiceCollection services)
        {
            foreach (var handlerType in EventHandlerTypes.Select(i=>i.HandlerType).Distinct().ToList()) 
	            services.AddTransient(handlerType);
        }


        internal Dictionary<string, EventListener> CreateEventListeners(IServiceProvider serviceProvider)
        {
	        var listeners = new Dictionary<string, EventListener>();
			foreach (var eventHandlerType in EventHandlerTypes)
			{
				listeners.TryGetValue(eventHandlerType.Channel, out var listener);
				if (listener == null)
				{
					listener = new EventListener(eventHandlerType.Channel, serviceProvider);
					listeners.Add(eventHandlerType.Channel, listener);
				}
				listener.RegisterEventHandler(eventHandlerType.HandlerType);
			}
            return listeners;
        }

    }

	public class EventHandlerType
	{
		public EventHandlerType(string channel, Type handlerType)
		{
			Channel = channel;
			HandlerType = handlerType;
		}

		public Type HandlerType { get; }
		public string Channel { get; }
	}
}

