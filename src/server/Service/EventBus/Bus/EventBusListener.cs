using StackExchange.Redis;

namespace Service.EventBus;

public class EventBusListener
{
	private readonly string _channel;
	private readonly EventListener _listener;
	private readonly StackExchange.Redis.ISubscriber _bus;


	public EventBusListener(EventListener listener, ConnectionMultiplexer connection)
	{
		_listener = listener;
		_channel = _listener.Channel;
		_bus = connection.GetSubscriber();
		_bus.Subscribe(_channel).OnMessage(_listener.OnMessageHandler);
	}


}