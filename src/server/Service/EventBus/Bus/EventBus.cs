using StackExchange.Redis;
using Core.EventBus;
using Core.Events;

namespace Service.EventBus
{
    public static class EventBus
	{
        private static StackExchange.Redis.ISubscriber _bus;

        private static StackExchange.Redis.ISubscriber bus
        {
	        get
	        {
				if(_bus == null)
					_bus = EventBusFactory.Connection.GetSubscriber();

				return _bus;
			}
        }

        public static async Task SendEventAsync<TEventBusChannel, TEvent>(TEvent message) 
	        where TEvent : IEvent<TEventBusChannel>
			where TEventBusChannel : IEventBusChannel
		{
            var msg = new EventBusMessageSerializer<TEvent>(message);
            await bus.PublishAsync(typeof(TEventBusChannel).Name, msg.Serialized, CommandFlags.FireAndForget);
        }

		public static void SendEvent<TEventBusChannel, TEvent>(TEvent message)
			where TEvent : IEvent<TEventBusChannel>
			where TEventBusChannel : IEventBusChannel
		{
			var msg = new EventBusMessageSerializer<TEvent>(message);
			bus.Publish(typeof(TEventBusChannel).Name, msg.Serialized, CommandFlags.FireAndForget);
		}


		public static async Task SendDelayedEventAsync<TEventBusChannel, TEvent>(TEvent message)
			where TEvent : IDelayedEvent<TEventBusChannel>
			where TEventBusChannel : IEventBusChannel
		{
			var msg = new EventBusMessageSerializer<TEvent>(message);
			await bus.PublishAsync(typeof(TEventBusChannel).Name, msg.Serialized, CommandFlags.FireAndForget);
		}

		public static void SendDelayedEvent<TEventBusChannel, TEvent>(TEvent message)
			where TEvent : IDelayedEvent<TEventBusChannel>
			where TEventBusChannel : IEventBusChannel
		{
			var msg = new EventBusMessageSerializer<TEvent>(message);
			bus.Publish(typeof(TEventBusChannel).Name, msg.Serialized, CommandFlags.FireAndForget);
		}

		internal static async Task SendStringMessageAsync(string channel, string message)
        {
            await bus.PublishAsync(channel, message, CommandFlags.FireAndForget);
        }
    }
}
