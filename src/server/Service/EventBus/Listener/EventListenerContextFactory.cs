using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus;

public class EventListenerContextFactory
{
	private static readonly AsyncLocal<EventListenerContextHolder> _eventListenerContextCurrent =
		new AsyncLocal<EventListenerContextHolder>();
	
	public EventListenerContext? EventListenerContext
	{
		get { return _eventListenerContextCurrent.Value?.Context; }
		set
		{
			var holder = _eventListenerContextCurrent.Value;
			if (holder != null)
			{
				holder.Context = null;
			}

			if (value != null)
			{
				_eventListenerContextCurrent.Value = new EventListenerContextHolder() { Context = value };
			}
		}
	}
	
	private class EventListenerContextHolder
	{
		public EventListenerContext? Context;
	}
}



	public class EventListenerContext : IDisposable
{
	private Guid _key = Guid.NewGuid();
	public Guid Key => _key ;

	private IServiceScope _serviceScope;
	public IServiceScope ServiceScope => _serviceScope;

	public EventListenerContext(IServiceScope serviceScope)
	{
		_serviceScope = serviceScope;
	}

	public void Dispose()
	{
		_serviceScope = null;
	}
}
