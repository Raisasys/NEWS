using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus
{
    public static class EventBusInstaller
    {
        public static IServiceCollection AddEventBusListeners(this IServiceCollection services,Action<EventListenerContainer> config)
        {
	        var eventBusConfiguration = new EventListenerContainer();
			config(eventBusConfiguration);

			services.AddSingleton<EventListenerContextFactory>();
			services.AddEventListener(eventBusConfiguration);
			services.AddTransient<IDelayedEventListenerInvokerJob, DelayedEventListenerInvokerJob>();
			
			//set in the last line
			services.AddHostedService<EventBusListenerWorker>();

            return services;

        }
        
	}

   
}

