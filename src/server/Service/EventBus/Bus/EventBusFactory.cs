using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus
{
	internal static class EventBusFactory
    {
        private static ConnectionMultiplexer _connection;
        private static EventListenerContainer _eventListenerContainer;
		internal static List<EventBusListener> EventBusListeners = new List<EventBusListener>();

		
		internal static ConnectionMultiplexer Connection
		{
			get
			{
				if(_connection == null)
					_connection ??= ConnectionMultiplexer.Connect(EventBusSetting.RedisConnectionString);
				return _connection;
			}
		}

		internal static IServiceCollection AddEventListener(this IServiceCollection services, EventListenerContainer eventListenerContainer)
        {
			_eventListenerContainer = eventListenerContainer;
			_eventListenerContainer.AddEventHandler(services);
			return services;
        }

		internal static void UseEventBusListener(IServiceProvider serviceProvider)
        {
            var listeners = _eventListenerContainer.CreateEventListeners(serviceProvider);
			foreach (var listener in listeners)
			{
				var eventBusListener = new EventBusListener(listener.Value, Connection);
				EventBusListeners.Add(eventBusListener);
			}
        }


	}
}
