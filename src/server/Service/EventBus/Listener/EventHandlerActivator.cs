using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus;

internal class EventHandlerActivator
{
	private readonly IServiceScope _serviceScope;

	public EventHandlerActivator(IServiceScope serviceScope)
	{
		_serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
		var rootContext = new EventListenerContext(_serviceScope);
		_serviceScope.ServiceProvider.GetRequiredService<EventListenerContextFactory>().EventListenerContext = rootContext;
	}

	public object Resolve(Type type) => _serviceScope.ServiceProvider.GetRequiredService(type);
	
	public void DisposeScope()
	{
		var contextFactory = _serviceScope.ServiceProvider.GetRequiredService<EventListenerContextFactory>();
		contextFactory.EventListenerContext?.Dispose();
		contextFactory.EventListenerContext = null;
		_serviceScope.Dispose();
	}
}